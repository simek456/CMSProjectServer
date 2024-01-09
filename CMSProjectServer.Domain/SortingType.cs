using System.Collections.Generic;

namespace CMSProjectServer.Domain;

public class SortingType
{
    public const string NameDescending = "NDesc";
    public const string NameAscending = "NAsc";
    public const string TimeDescending = "NTDesc";
    public const string TimeAscending = "TAsc";
    public const string LikeDescending = "LDesc";
    public const string LikeAscending = "LAsc";

    public static readonly List<string> AvailableSortings = new List<string>() { NameAscending, NameDescending, TimeAscending, TimeDescending, LikeDescending, LikeAscending };
}