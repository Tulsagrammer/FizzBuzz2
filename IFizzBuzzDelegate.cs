﻿using System;

namespace FizzBuzz2
{
    public interface IFizzBuzzDelegate
    {
        void TestStart(string testFunction);
        void TestFinish();
        void TestItem(int value, string tag);
        void ResultsStart();
        void ResultsFinish();
        void ResultsItem(TimeSpan timeSpan, string testFunction);
    }
}
