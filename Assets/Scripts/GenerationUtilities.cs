using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GenerationUtilities {

    public static readonly List<string> names = new [] {
        "Tom Shaw", "Stanley Wilkinson", "George Murphy", "Harley Hopkins", "Ellis Moore", "Taylor Herbert",
        "Terrance Mack", "Casen Langley", "Peter Whitley", "Rylen Downs", "Joshua Bailey", "Morgan Lee", "Arthur Fox",
        "Theo Wallace", "Spencer Holland", "Cael Foster", "Caleb Avila", "Hector Shaffer", "Thiago Mayer", "Van Alford",
        "Ellis Hall", "Oscar Harper", "Ethan Taylor", "Arthur Carter", "Connor Holland", "Ahmad Beach", "Karsen Mcleod",
        "Axel White", "Trent Vance", "Ameer Albert", "Abby John", "Alisha Cox", "Bethany Wells", "Jasmine White",
        "Faith Burns", "Lydia Cain", "Brinley Poole", "Marlene Mccray", "Summer Leonard", "Kayden Sharpe",
        "Laura Sutton", "Mia Hopkins", "Demi Fletcher", "Keira Stevens", "Layla Webb", "Lena West", "Talia Richard",
        "Zariah Chapman", "Corinne Knapp", "Mary Nelson"
    }.ToList();

    public static string GenerateName(Language lang) {
        return names.GetRand();
    }

    public static string GenerateAddress(Language lang) {
        var rand = new System.Random();

        switch (lang) {
            case Language.English:
                return rand.Next(1, 99)
                       + " " + new[] {
                               "First Street", "Second Street", "Third Street", "Fourth Street", "Fifth Street",
                               "Green Street", "Red Street", "Blue Street", "Great Street", "Main Street"
                           }
                           .ToList().GetRand();
            case Language.German:
                return rand.Next(1, 99)
                       + " " + new[] {
                               "Erste Straße", "Zweite Straße", "Dritte Straße", "Vierte Straße", "Fünfte Straße",
                               "Grüne Straße", "Rote Straße", "Blaue Straße", "Großstraße", "Hauptstraße"
                           }
                           .ToList().GetRand();
            case Language.Bulgarian:
                return rand.Next(1, 99)
                       + " " + new[] {
                           "Първа Улица", "Втора Улица", "Трета Улица", "Четвърта Улица", "Пета Улица", "Зелена Улица",
                           "Червена Улица", "Синя Улица", "Голяма Улица", "Главна Улица"
                       }.ToList().GetRand();
            case Language.Norwegian:
                return rand.Next(1, 99)
                        + " " + new[] {
                            "Første Gate", "Andre Gate", "Tredje Gate", "Fjerde Gate", "Femte Gate", "Grønngata",
                            "Rødgate", "Blågate", "Storgate", "Hovedgate"
                        }.ToList().GetRand();
            case Language.Russian:
                return rand.Next(1, 99)
                        + " " + new[] {
                           "Первая Улица", "Вторая Улица", "Третья Улица", "Четвертая Улица", "Пятая Улица", "Зеленая Улица",
                           "Красная Улица", "Синяя Улица", "Большая Улица", "Главная Улица"
                        }.ToList().GetRand();
            case Language.Chinese:
                return rand.Next(1, 99)
                       + " " + new[] {
                           "第一街", "第二街道", "第三街", "第四街", "第五街", "绿色的街道",
                           "红街", "蓝色的街道", "大街", "主要街道"
                       }.ToList().GetRand();
            default:
                throw new ArgumentOutOfRangeException(nameof(lang), lang, null);
        }
    }


    private static string[] english = new[]
        {"Little Barnton", "Everbarrow", "Great Hatchmill", "Great Waterset"};
    private static string[] englishCountries = new[]
        {"England", "UK", "United Kingdom", "Great Britain"};
    private static string[] englishVoids = new[] { "London, Britain", "Shenzhen, China", "Bristol, England", "Pwllheli, Wales", "New York City, USA", "Sofia, Bulgaria" };

    private static string[] bulgarian = new[]
        {"Малко Хамбарово", "Вечна Могила", "Голяма Шлюзомелница", "Голям Водозалез"};
    private static string[] bulgarianCountries = new[]
        {"Англия", "Великобритания", "Обединено Кралство", "Обединеното Кралство"};
    private static string[] bulgarianVoids = new[] {"Лондон, Великобритания", "София, България", "Бристол, Обединено Кралство", "Бристол, Великобритания",
        "Глазгоу, Великобритания", "Глазгоу, Шотландия", "Ню Йорк, САЩ", "Шънджън, Китай"};

    private static string[] german = new[]
        {"Kleines Scheunendorf", "Immerhügel", "Große Deckelmühle", "Großer Wasseruntergang"};
    private static string[] germanCountries = new[]
        {"England", "Großbritannien", "Großbritannien", "Großbritannien"};
    private static string[] germanVoids = new[] {"London, Großbritannien", "Köln, Deutschland", "Bristol, England", "Rostock, Mecklenburg-Vorpommern, Deutschland",
        "Basel, die Schweiz", "Glasgow, Schottland", "Shenzhen, Volksrepublic China"};

    private static string[] norwegian = new[]
        {"Lille Låveby", "Evighaug", "Store Lokkmølle", "Stort Vannsett"};
    private static string[] norwegianCountries = new[]
        {"England", "Storbritannia", "Storbritannia", "Storbritannia"};
    private static string[] norwegianVoids = new[] {"Stavanger, Rogaland, Norge", "Reykjavik, Island", "Bristol, Storbritannien", "Wien, Østerrike",
        "Edinburgh, Skottland", "Shenzhen, Folkerepubliken Kina", "Shenzhen, Kina"};

    private static string[] russian = new[]
        {"Маленкое Амбарово", "Вечньй Курган", "Большая Люкомельница", "Большой Водной Закат"};
    private static string[] russianCountries = new[]
        {"Англия", "Великобритания", "Объединенное Королевство"};
    private static string[] russianVoids = new[] {"Лондон, Великобритания", "Омск, Россия", "Бристол, Великобритания", "Вена, Австрия",
        "Глазго, Шотландия", "Иокогама, Япония", "Новый Йорк, США", "Шэньчжэнь, Китай"};

    private static string[] chinese = new[]
        {"小谷仓村", "永恒冢", "大舱口磨", "大水日落"};
    private static string[] chineseCountries = new[]
        {"英国", "大不列颠", "联合王国"};
    private static string[] chineseVoids = new[] {"伦敦, 英国", "鄂木斯克, 俄国", "布里斯托尔, 大不列颠", "维也纳, 奥地利",
        "爱丁堡, 苏格兰", "横滨, 日本", "纽约, 美利坚合众国", "深圳, 中国"};


    public static string GenerateDestName(Language lang, Destination dest) {
        var i = (int)dest;

        if (dest == Destination.Void) {
            switch (lang) {
                case Language.English:
                    return englishVoids.ToList().GetRand();
                case Language.German:
                    return germanVoids.ToList().GetRand();
                case Language.Bulgarian:
                    return bulgarianVoids.ToList().GetRand();
                case Language.Norwegian:
                    return norwegianVoids.ToList().GetRand();
                  case Language.Russian:
                      return russianVoids.ToList().GetRand();
                  case Language.Chinese:
                      return chineseVoids.ToList().GetRand();
                default:
                    throw new ArgumentOutOfRangeException(nameof(lang), lang, null);
            }
        }
        else switch (lang) {
                case Language.English:
                    return GameManager.DestToStr(dest) + ", " + englishCountries.ToList().GetRand(); // I know, I know, this is horrible
                case Language.German:
                    return german[i] + ", " + germanCountries.ToList().GetRand();
                case Language.Bulgarian:
                    return bulgarian[i] + ", " + bulgarianCountries.ToList().GetRand();
                case Language.Norwegian:
                    return norwegian[i] + ", " + norwegianCountries.ToList().GetRand();
                case Language.Russian:
                    return russian[i] + ", " + russianCountries.ToList().GetRand();
                case Language.Chinese:
                    return chinese[i] + ", " + chineseCountries.ToList().GetRand();
                default:
                    throw new ArgumentOutOfRangeException(nameof(lang), lang, null);
            }
    }
}

public static class StackOverflowSourcedExtensions {
    public static T GetRand<T>(this List<T> enumerable) {
        var index = new System.Random().Next(0, enumerable.Count);
        return enumerable[index];
    }

    public static double NextDouble(this System.Random rand, double minimum, double maximum) {
        return rand.NextDouble() * (maximum - minimum) + minimum;
    }
}
