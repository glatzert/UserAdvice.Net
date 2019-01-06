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

        public override void Init(TagHelperContext context)
        {
            base.Init(context);

            var ctx = ConditionalContext.GetFromContext(context);
            ctx.NeedsElseContent = ShouldSuppress();
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            output.Attributes.RemoveAll("condition");

            if (context.TagName.Equals("if", StringComparison.OrdinalIgnoreCase))
                output.TagName = null;

            var ctx = ConditionalContext.GetFromContext(context);
            var innerContent = await output.GetChildContentAsync();

            if (ctx.NeedsElseContent)
            {
                if (ctx.ElseContent == null)
                    output.SuppressOutput();
                else
                    output.Content.SetHtmlContent(ctx.ElseContent);
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

            if (ctx.NeedsElseContent)
            {
                ctx.ElseContent = await output.GetChildContentAsync();
            }

            output.SuppressOutput();
        }
    }

    class ConditionalContext
    {
        public IHtmlContent ElseContent { get; set; }
        public bool NeedsElseContent { get; internal set; }

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
