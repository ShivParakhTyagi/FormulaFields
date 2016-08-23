using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PortableSource
{
    class formItemObjCopy
    {

        #region Source

        MySpecialClass _specialClass = new MySpecialClass()
        {
            DataClass = new Data()
            {
                A = "asc",
                B = 12
            },

            FormName = "name",

            Data = "daataa",

            FormDataName = new Data()
            {
                A = "asd",
                B = 21321
            },

            ParentDataList = new List<string>(),

            DataList = new List<string>(),
            ParentDataClassList = new List<Data>()
            {
                new Data()
                {
                    A = "ads",
                    B = 123
                }
            },

            DataClassList = new List<Data>()
            {
                new Data()
                {
                    A = "ads",
                    B = 123
                }
            },

            FieldKeys = new List<Data>()
            {
                new Data() {TYPE = "Number"},
                new Data() {TYPE = "FilePicker"},
                new Data() {TYPE = "ImagePicker"},
                new Data() {TYPE = "GeoStamp"},
                new Data() {TYPE = "Lookup"},
                new Data() {TYPE = "LookupMulti"},
                new Data() {TYPE = "Guid"},
                new Data() {TYPE = "Text"},
                new Data() {TYPE = "Id"},
                new Data() {TYPE = "DateTimeInfo"},
                new Data() {TYPE = "TimeSpan"},
                new Data() {TYPE = "Boolean"},
                new Data() {TYPE = "SubForm"},
            },

            FieldValues = new Dictionary<string, object>()
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
                {
                    "6",
                    new DataStructs.MultiLookup(new List<int>() {1, 2, 3, 4}, new List<string>() {"A", "B", "C", "D"})
                },
                {"7", Guid.NewGuid()},
                {"8", "data"},
                {"9", 10},
                {"10", DateTime.MaxValue},
                {"11", TimeSpan.MaxValue},
                {"12", true},
                //{
                //    "13",
                //    new List<Data>()
                //    {
                //        new Data() {A = "123", B = 123},
                //        new Data() {A = "A123", B = 23},
                //        new Data() {A = "B123", B = 3},
                //    }
                //},
                {
                    "14", new MySpecialClass()
                    {

                        DataClass = new Data()
                        {
                            A = "ftgh",
                            B = 1223
                        },

                        FormName = "SUB",

                        Data = "daawrwerwetaa",

                        FormDataName = new Data()
                        {
                            A = "avew23sd",
                            B = 213212
                        },

                        ParentDataList = null,

                        DataList = null,
                        ParentDataClassList = new List<Data>()
                        {
                            new Data()
                            {
                                A = "adsdwd",
                                B = 123
                            }
                        },

                        DataClassList = new List<Data>()
                        {
                            new Data()
                            {
                                A = "adwdwds",
                                B = 123
                            }
                        },

                        FieldKeys = new List<Data>()
                        {
                            new Data() {TYPE = "Number"},
                            new Data() {TYPE = "FilePicker"},
                            new Data() {TYPE = "ImagePicker"},
                            new Data() {TYPE = "GeoStamp"},
                            new Data() {TYPE = "Lookup"},
                            new Data() {TYPE = "LookupMulti"},
                            new Data() {TYPE = "Guid"},
                            new Data() {TYPE = "Text"},
                            new Data() {TYPE = "Id"},
                            new Data() {TYPE = "DateTimeInfo"},
                            new Data() {TYPE = "TimeSpan"},
                            new Data() {TYPE = "Boolean"},
                        },

                        FieldValues = new Dictionary<string, object>()
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
                            //{
                            //    "x13",
                            //    new List<Data>()
                            //    {
                            //        new Data() {A = "123", B = 123},
                            //        new Data() {A = "A123", B = 23},
                            //        new Data() {A = "B123", B = 3},
                            //    }
                            //}
                        }
                    }
                },
            }
        };

        #endregion

        public void Copy()
        {
            MySpecialClass shallowCopy = _specialClass;
            MySpecialClass deepCopy=new MySpecialClass();

            deepCopy = shallowCopy.Copy();
        }

    }
}