using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonTCG.Karten
{
    class PokemonKarte : Karte
    {

        public string vorentwicklung;
        public string weiterentwicklung;

        public string typ;
        public int kp;

        public string angriff1;
        public int kosten1;
        public string energie1;
        public int farblos1;
        public int schaden1;
        public bool fähigkeit1;

        public string angriff2;
        public int kosten2;
        public string energie2;
        public int farblos2;
        public int schaden2;
        public bool fähigkeit2;

        public string schwäche;
        public string resistenz;

        public int rückzugskosten;
        public int dexNummer;


        public PokemonKarte(int id, string art, string nam, bool fäh, int num, string booster,
            string vor,string weiter,string typ, int kp, string a1,int k1,string e1, int f1, int s1, bool fä1,
            string a2, int k2, string e2, int f2, int s2, bool fä2,string schw,string res,int rück,int dex)
        {

            this.ID = id;
            this.kartenArt = art;
            this.kartenName = nam;
            this.fähigkeit = fäh;
            this.kartenNummer = num;
            this.booster = booster;
            this.vorentwicklung = vor;
            this.weiterentwicklung = weiter;
            this.typ = typ;
            this.kp = kp;
            this.angriff1 = a1;
            this.kosten1 = k1;
            this.energie1 = e1;
            this.farblos1 = f1;
            this.schaden1 = s1;
            this.fähigkeit1 = fä1;
            this.angriff2 = a2;
            this.kosten2 = k2;
            this.energie2 = e2;
            this.farblos2 = f2;
            this.schaden2 = s2;
            this.fähigkeit2 = fä2;
            this.schwäche = schw;
            this.resistenz = res;
            this.rückzugskosten = rück;
            this.dexNummer = dex;
        }


    }
}
