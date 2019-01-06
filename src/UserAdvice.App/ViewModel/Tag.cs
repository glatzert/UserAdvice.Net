using System;

namespace UserAdvice.ViewModel
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string BackgroundColor { get; set; }
        public string ForegroundColor
        {
            get
            {
                var red = Convert.ToInt32(BackgroundColor.Substring(0, 2));
                var green = Convert.ToInt32(BackgroundColor.Substring(2, 2));
                var blue = Convert.ToInt32(BackgroundColor.Substring(4, 2));

                double luminance = (0.299 * red + 0.587 * green + 0.114 * blue) / 255;

                if (luminance > 0.5)
                    return "000000";
                else
                    return "FFFFFF";
            }
        }

        public bool IsPublic { get; set; }
    }
}
