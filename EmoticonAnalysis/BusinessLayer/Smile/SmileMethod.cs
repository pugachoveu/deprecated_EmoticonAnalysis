﻿using Newtonsoft.Json;
using Smile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Smile
{
    public class SmileMethod
    {
        private static SmileMethod instance;
        private static List<EmoticonModel> emoticons;

        private SmileMethod()
        {
            emoticons = LoadEmoticons();
        }

        public static SmileMethod Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SmileMethod();
                }
                return instance;
            }
        }

        public string Analyze(string message)
        {
            //var emoticonList = LoadEmoticons();
            int meaningValue = 0;
            var messArr = message.Split(' ');

            foreach (var e in messArr)
            {
                var t = emoticons.Where(c => c.Emoji.Contains(e));
                if (t.Any())
                {
                    meaningValue += t.First().Polarity;
                }
            }
            return meaningValue > 0 ? "positive" : "negative";
        }

        private List<EmoticonModel> LoadEmoticons()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/emoticon_list.txt");
            if (!File.Exists(path))
                return null;

            var fileJsonData = File.ReadAllText(path);

            var emoticonList = JsonConvert.DeserializeObject<List<EmoticonModel>>(fileJsonData);

            return emoticonList;
        }
    }
}
