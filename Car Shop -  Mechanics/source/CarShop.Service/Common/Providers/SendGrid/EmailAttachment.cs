﻿namespace CarShop.Service.Common.Providers.SendGrid
{
    public class EmailAttachment
    {
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
    }
}