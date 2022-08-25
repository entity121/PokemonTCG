using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PokemonTCG.Karten
{
    class Karte
    {
        //VARIABLEN
        //#############################
        public int I_ID;
        public string S_art;
        public string S_kartenname;
        public string S_vorentwicklung;
        public string S_weiterentwicklung;
        public string S_typ;
        public int I_kp;
        public int I_fähigkeit;
        public string S_angriff1;
        public int I_kosten1;
        public string S_energie1;
        public int I_farblos1;
        public int I_schaden1;
        public int I_fähigkeit1;
        public string S_angriff2;
        public int I_kosten2;
        public string S_energie2;
        public int I_farblos2;
        public int I_schaden2;
        public int I_fähigkeit2;
        public string S_schwäche;
        public string S_resistenz;
        public int I_rückzugskosten;
        public int I_dexNummer;
        public int I_kartenNummer;
        public string S_booster;
        public int I_basisEnergie;

        public List<string> Ls_energieAngelegt;
        //#############################


        //KONSTRUKTOR
        //###########################################################
        public Karte(
                int id, string art, string nam, string vor, string weiter, string typ, int kp, int fäh,
                string a1, int k1, string e1, int f1, int s1, int fä1,
                string a2, int k2, string e2, int f2, int s2, int fä2,
                string schw, string res, int rück, int dex, int num, string booster, int bas)
        {

            this.I_ID = id;
            this.S_art = art;
            this.S_kartenname = nam;
            this.S_vorentwicklung = vor;
            this.S_weiterentwicklung = weiter;
            this.S_typ = typ;
            this.I_kp = kp;
            this.I_fähigkeit = fäh;
            this.S_angriff1 = a1;
            this.I_kosten1 = k1;
            this.S_energie1 = e1;
            this.I_farblos1 = f1;
            this.I_schaden1 = s1;
            this.I_fähigkeit1 = fä1;
            this.S_angriff2 = a2;
            this.I_kosten2 = k2;
            this.S_energie2 = e2;
            this.I_farblos2 = f2;
            this.I_schaden2 = s2;
            this.I_fähigkeit2 = fä2;
            this.S_schwäche = schw;
            this.S_resistenz = res;
            this.I_rückzugskosten = rück;
            this.I_dexNummer = dex;
            this.I_kartenNummer = num;
            this.S_booster = booster;
            this.I_basisEnergie = bas;

            this.Ls_energieAngelegt = new List<string>();
        }
        //###########################################################




        //###########################################################
        public void Energie_Anlegen(string energie)
        {
            this.Ls_energieAngelegt.Add(energie);
        }
        //###########################################################






        //###########################################################
        public void Energie_Zeigen()
        {
            if(this.Ls_energieAngelegt.Count > 0)
            {

                for(int i = 0; i < Ls_energieAngelegt.Count; i++)
                {
                    System.Windows.MessageBox.Show(this.Ls_energieAngelegt[i].ToString());
                }

            }
            else
            {
                System.Windows.MessageBox.Show("Keine Energie");
            }
            
        }
        //###########################################################

    }
}
