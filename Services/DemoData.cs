using System;
using ObjektLab1.Models;

namespace ObjektLab1.Services
{
    public static class DemoData
    {
        public static Forening SkapaDemoForening()
        {
            var forening = new Forening();

            var m1 = new StandardMedlem(1, "Björn", "bjorn@bradhornan.se", "0701111111");
            var m2 = new StandardMedlem(2, "Amira", "amira@bradhornan.se", "0702222222");
            var m3 = new StandardMedlem(3, "Manar", "manar@bradhornan.se", "0703333333");
            var m4 = new HedersMedlem(4, "Elmi", "elmi@bradhornan.se", "0704444444");

            forening.Medlemmar.Add(m1);
            forening.Medlemmar.Add(m2);
            forening.Medlemmar.Add(m3);
            forening.Medlemmar.Add(m4);

            var uno = new Spel("Uno", "Kortspel", 2, 10);
            var chess = new Spel("Chess", "Strategi", 2, 2);
            var yatzyLiten = new Spel("Yatzy", "Tärningsspel", 2, 4);
            var yatzyStor = new Spel("Yatzy", "Tärningsspel", 2, 10);

            forening.Spel.Add(uno);
            forening.Spel.Add(chess);
            forening.Spel.Add(yatzyLiten);
            forening.Spel.Add(yatzyStor);

            var t1 = new SpelTraff(
                "Nybörjarkväll",
                "Spelrum,1",
                2,
                10,
                DateTime.Today.AddDays(0).AddHours(18),
                uno);

            var t2 = new SpelTraff(
                "Strategikväll",
                "Spelrum, 2",
                2,
                4,
                DateTime.Today.AddDays(1).AddHours(19),
                yatzyLiten);

            var t3 = new SpelTraff(
                "Strategikväll",
                "Spelrum, 3",
                2,
                10,
                DateTime.Today.AddDays(2).AddHours(20),
                yatzyStor);

            t1.Anmal(m1);
            t2.Anmal(m2);

            forening.Speltraffar.Add(t1);
            forening.Speltraffar.Add(t2);
            forening.Speltraffar.Add(t3);

            return forening;
        }
    }
}
