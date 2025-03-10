﻿namespace MOGASite.Core.DTOs.Requests
{
    public class AddReviewRequest
    {
        public string FirstNameEN { get; set; } = string.Empty;
        public string FirstNameAR { get; set; } = string.Empty;

        public string LastNameEN { get; set; } = string.Empty;
        public string LastNameAR { get; set; } = string.Empty;

        public string PositionEN { get; set; } = string.Empty;
        public string PositionAR { get; set; } = string.Empty;

        public string ReviewTextEN { get; set; } = string.Empty;
        public string ReviewTextAR { get; set; } = string.Empty;

        public int Stars { get; set; } = 0;

        public IFormFile? Image { get; set; }
    }
}
