using System;
using System.Collections.Generic;

namespace PortableSource
{
    public class TestCopy
    {
        Dictionary<string, object> sourceDictionary = new Dictionary<string, object>()
        {
            {"1", 12.4},
            {
                "2", new DataStructs.File()
                {
                    Content = "AABC==",
                    LocalLink = "MMXX//::XXSSS//SSS",
                    Name = "SASA",
                    ServerLink = "SAAS??SASA//ASSAs",
                    Size = 1212,
                    State = "D",
                }
            },
            {
                "3", null
            },
            {
                "4", new DataStructs.GeoLocation
                {
                    Latitude = "N,casd002121",
                    Longitude = "S*123123"
                }
            },
            {"5", new DataStructs.Lookup(1, "data")},
            {"6", new DataStructs.MultiLookup(new List<int>() {1, 2, 3, 4}, new List<string>() {"A", "B", "C", "D"})},
            {"7", Guid.NewGuid()},
            {"8", "data"},
            {"9", 10},
            {"10", DateTime.MaxValue},
            {"11", TimeSpan.MaxValue},
            {"12", true},
            {
                "13",
                new List<Data>()
                {
                    new Data() {A = "123", B = 123},
                    new Data() {A = "A123", B = 23},
                    new Data() {A = "B123", B = 3},
                }
            },
            {
                "14", new Dictionary<string, object>()
                {
                    {"x1", 12.4},
                    {
                        "x2", new DataStructs.File()
                        {
                            Content = "AABC==",
                            LocalLink = "MMXX//::XXSSS//SSS",
                            Name = "SASA",
                            ServerLink = "SAAS??SASA//ASSAs",
                            Size = 1212,
                            State = "D",
                        }
                    },
                    {
                        "x3", null
                    },
                    {
                        "x4", new DataStructs.GeoLocation
                        {
                            Latitude = "N,casd002121",
                            Longitude = "S*123123"
                        }
                    },
                    {"x5", new DataStructs.Lookup(1, "data")},
                    {
                        "x6",
                        new DataStructs.MultiLookup(new List<int>() {1, 2, 3, 4},
                            new List<string>() {"A", "B", "C", "D"})
                    },
                    {"x7", Guid.NewGuid()},
                    {"x8", "data"},
                    {"x9", 10},
                    {"x10", DateTime.MaxValue},
                    {"x11", TimeSpan.MaxValue},
                    {"x12", true},
                    {
                        "x13",
                        new List<Data>()
                        {
                            new Data() {A = "123", B = 123},
                            new Data() {A = "A123", B = 23},
                            new Data() {A = "B123", B = 3},
                        }
                    }
                }
            },
        };

        public void Run()
        {
            Dictionary<string, object> shallowCopy = new Dictionary<string, object>(sourceDictionary);
            Dictionary<string, object> deepCopy = new Dictionary<string, object>();

            foreach (var data in shallowCopy)
            {
            }
        }



    }
}