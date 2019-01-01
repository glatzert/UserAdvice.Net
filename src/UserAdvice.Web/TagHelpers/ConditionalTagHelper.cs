using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAdvice.Web.TagHelpers
{
    [HtmlTargetElement(Attributes = "condition")]
    [HtmlTargetElement("if", Attributes = "condition")]
    public class ConditionalIfTagHelper : TagHelper
    {
        public object Condition { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            var ctx = ConditionalContext.GetFromContext(context);
            var innerContent = await output.GetChildContentAsync();

            if (ShouldSuppress())
            {
                if (ctx.ElseContent == null)
                    output.SuppressOutput();
                else
                    output.Content.SetHtmlContent(ctx.ElseContent);
            }
            else
            {
                if (context.TagName.Equals("if", StringComparison.OrdinalIgnoreCase))
                    output.Content.SetHtmlContent(innerContent);
            }
        }

        private bool ShouldSuppress()
        {
            return Condition is null || Condition is 0 || Condition is false ||
                (Condition is string s && string.IsNullOrWhiteSpace(s)) ||
                (Condition is IEnumerable<object> e && !e.Any());
        }
    }

    [HtmlTargetElement("else", ParentTag = "if")]
    public class ConditionalElseTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var ctx = ConditionalContext.GetFromContext(context);

            ctx.ElseContent = await output.GetChildContentAsync();
            output.SuppressOutput();
        }
    }

    class ConditionalContext
    {
        public IHtmlContent ElseContent { get; set; }

        public static ConditionalContext GetFromContext(TagHelperContext context)
        {
            if (!context.Items.TryGetValue(nameof(ConditionalContext), out var ctx))
            {
                ctx = new ConditionalContext();
                context.Items.Add(nameof(ConditionalContext), ctx);
            }

            return (ConditionalContext)ctx;
        }
    }
}
