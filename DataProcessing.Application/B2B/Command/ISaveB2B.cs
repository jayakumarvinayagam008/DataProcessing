﻿namespace DataProcessing.Application.B2B.Command
{
    public interface ISaveB2B
    {
        B2BSaveSummary Save(string filePath);
    }
}