using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Wallet
    {
        //Member Variables (Has A)
        public List<Coin> Coins;
        //Constructor (Spawner)
        public Wallet()
        {
            Coins = new List<Coin>();
            
            FillRegister();
        }
        //Member Methods (Can Do)
        //Fills wallet with starting money
        private void FillRegister()
        {
            for (int i = 0; i < 14; i++) //3.50
            {
                Coin quarter = new Quarter();
                Coins.Add(quarter);
            }

            for (int i = 0; i < 10; i++) //1.50
            {
                Coin dime = new Dime();
                Coins.Add(dime);

                Coin nickle = new Nickel();
                Coins.Add(nickle);
            }

            for (int i = 0; i < 5; i++)
            {
                Coin penny = new Penny();
                Coins.Add(penny);
            }
        }
    }
}
