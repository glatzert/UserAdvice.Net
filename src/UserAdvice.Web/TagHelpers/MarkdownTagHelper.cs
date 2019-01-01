using Markdig;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace UserAdvice.Web.TagHelpers
{
    [HtmlTargetElement("markdown", Attributes = "source", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class MarkdownTagHelper : TagHelper
    {
        public string Source { get; set; }

        public MarkdownMode Mode { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            var pipeline = (Mode == MarkdownMode.Simple)
                ? SimpleMarkdown
                : FullMarkdown;

            var renderedMarkdown = Markdown.ToHtml(Source, pipeline);

            output.TagName = null;
            output.Content.SetHtmlContent(renderedMarkdown);
        }

        static MarkdownTagHelper()
        {
            SimpleMarkdown = new MarkdownPipelineBuilder()
                .DisableHtml()
                .UseDefinitionLists()
                .UseEmphasisExtras()
                .UseListExtras()
                .Build();

            FullMarkdown = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();
        }

        private static readonly MarkdownPipeline SimpleMarkdown;
        private static readonly MarkdownPipeline FullMarkdown;
    }

    public enum MarkdownMode
    {
        Simple,
        Full
    }
}
