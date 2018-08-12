using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GenerationUtilities {

    public static string GenerateName(Language lang) {
        return "J. Jonah Jameson";
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
        {"Малко Хамбарово", "Вечна Могила", "Голяма Капакомелница", "Голям Водозалез"};
    private static string[] bulgarianCountries = new[]
        {"Англия", "Великобритания", "Обединено Кралство", "Обединеното Кралство"};
    private static string[] bulgarianVoids = new[] {"Лондон, Великобритания", "София, България", "Бристол, Обединено Кралство", "Бристол, Великобритания",
        "Глазгоу, Великобритания", "Глазгоу, Шотландия", "Ню Йорк, САЩ", "Шънджън, Китай"};


    public static string GenerateDestName(Language lang, Destination dest) {
        var i = (int)dest;

        if (dest == Destination.Void) {
            switch (lang) {
                case Language.English:
                    return englishVoids.ToList().GetRand();
                /* case Language.German:
                     break;*/
                case Language.Bulgarian:
                    return bulgarianVoids.ToList().GetRand();
                /*  case Language.Norwegian:
                      break;
                  case Language.Russian:
                      break;
                  case Language.Chinese:
                      break;*/
                default:
                    throw new ArgumentOutOfRangeException(nameof(lang), lang, null);
            }
        }
        else switch (lang) {
                case Language.English:
                    return GameManager.DestToStr(dest) + ", " + englishCountries.ToList().GetRand(); // I know, I know, this is horrible
                                                                                                     /* case Language.German:
                                                                                                          break;*/
                case Language.Bulgarian:
                    return bulgarian[i] + ", " + bulgarianCountries.ToList().GetRand();
                /* case Language.Norwegian:
                    break;
                case Language.Russian:
                    break;
                case Language.Chinese:
                    break;*/
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
