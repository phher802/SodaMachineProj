using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Customer
    {
        //Member Variables (Has A)
        public Wallet Wallet;
        public Backpack Backpack;
        public List<Coin> coinsForPayment;

        //Constructor (Spawner)
        public Customer()
        {
            Wallet = new Wallet();
            Backpack = new Backpack();
            coinsForPayment = new List<Coin>();


        }
        //Member Methods (Can Do)

        //This method will be the main logic for a customer to retrieve coins form their wallet.
        //Takes in the selected can for price reference
        //Will need to get user input for coins they would like to add.
        //When all is said and done this method will return a list of coin objects that the customer will use a payment for their soda.
        public List<Coin> GatherCoinsFromWallet(Can selectedCan)
        {
            string getCoins;


            for (int i = 0; i < 10; i++)
            {

                getCoins = UserInterface.CoinSelection(selectedCan, Wallet.Coins);
                coinsForPayment.Add(GetCoinFromWallet(getCoins));
                bool willAddMoreCoins = UserInterface.ContinuePrompt("Would you like to add more coins? (y/n)");
                if (willAddMoreCoins == false)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            return coinsForPayment;

        }
        //Returns a coin object from the wallet based on the name passed into it.
        //Returns null if no coin can be found
        public Coin GetCoinFromWallet(string coinName)
        {

            Coin coinToRemove = null;

            for (int i = 0; i < Wallet.Coins.Count; i++)
            {

                if (coinName == Wallet.Coins[i].Name)
                {
                    coinToRemove = Wallet.Coins[i];
                    Wallet.Coins.Remove(coinToRemove);
                    //once its found, dont loop anymore
                    break;
                }


            }

            return coinToRemove;
        }
        //Takes in a list of coin objects to add into the customers wallet.
        public void AddCoinsIntoWallet(List<Coin> coinsToAdd)
        {
            //deposit change from sodamachine gatherChange method? or where is the list?
            // how to grab that change?

            //Wallet.Coins.Add();
            for (int i = 0; i < coinsToAdd.Count; i++)
            {
                Wallet.Coins.Add(coinsToAdd[i]);
            }

        }
        //Takes in a can object to add to the customers backpack.
        public void AddCanToBackpack(Can purchasedCan)
        {

            Backpack.cans.Add(purchasedCan);

        }

        public void CheckContentsOfBackpack()
        {
           

            for (int i = 0; i < Backpack.cans.Count; i++)
            {
                if (Backpack.cans.Count == 1)
                {
                    Console.WriteLine($"You have a {Backpack.cans[i].Name} in your backpack");
                }
                else
                {
                    Console.WriteLine($"You have {Backpack.cans.Count} cans in our backpack. They are:\n");
                    Console.WriteLine(Backpack.cans[i].Name);
                        
                }
                
            }


        }

        public void HowMuchCoinsLeftInWallet()
        {
            UserInterface.DiplayTotalValueOfCoins(Wallet.Coins);
        }
    }
}
