﻿namespace Orla.Core.Requests.YouTube;

public class UpdateYouTubeDetailRequest
{
    public string Id { get; set; }
    public string? Title { get; set; }
    public string? Link { get; set; }
    public string? Thumbnail { get; set; }
}
