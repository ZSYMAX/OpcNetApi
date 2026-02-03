using System;

namespace OpcRcw.Dx
{
    public enum Mask
    {
        None,
        ItemPath,
        ItemName,
        Version = 4,
        BrowsePaths = 8,
        Name = 16,
        Description = 32,
        Keyword = 64,
        DefaultSourceItemConnected = 128,
        DefaultTargetItemConnected = 256,
        DefaultOverridden = 512,
        DefaultOverrideValue = 1024,
        SubstituteValue = 2048,
        EnableSubstituteValue = 4096,
        TargetItemPath = 8192,
        TargetItemName = 16384,
        SourceServerName = 32768,
        SourceItemPath = 65536,
        SourceItemName = 131072,
        SourceItemQueueSize = 262144,
        UpdateRate = 524288,
        DeadBand = 1048576,
        VendorData = 2097152,
        ServerType = 4194304,
        ServerURL = 8388608,
        DefaultSourceServerConnected = 16777216,
        All = 2147483647
    }
}