using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachine
    {
        //Member Variables (Has A)
        private List<Coin> _register;
        private List<Can> _inventory;
        private List<Coin> changeToDispense;

        //Constructor (Spawner)
        public SodaMachine()
        {
            _register = new List<Coin>();
            _inventory = new List<Can>();
            FillInventory();
            FillRegister();
           
        }

        //Member Methods (Can Do)

        //A method to fill the sodamachines register with coin objects.
        public void FillRegister()
        {
            for (int i = 0; i < 20; i++)
            {
                Coin quarter = new Quarter();
                _register.Add(quarter);

                Coin nickle = new Nickel();
                _register.Add(nickle);
            }

            for (int i = 0; i < 10; i++)
            {
                Coin dime = new Dime();
                _register.Add(dime);
            }


            for (int i = 0; i < 50; i++)
            {
                Coin penny = new Penny();
                _register.Add(penny);
            }





        }
        //A method to fill the sodamachines inventory with soda can objects.
        public void FillInventory()
        {

            for (int i = 0; i < 20; i++)
            {
                Can cola = new Cola();
                _inventory.Add(cola);

                Can orangeSoda = new OrangeSoda();
                _inventory.Add(orangeSoda);

                Can rootBeer = new RootBeer();
                _inventory.Add(rootBeer);
            }



        }
        //Method to be called to start a transaction.
        //Takes in a customer which can be passed freely to which ever method needs it.
        public void BeginTransaction(Customer customer)
        {
            bool willProceed = UserInterface.DisplayWelcomeInstructions(_inventory);
            if (willProceed)
            {
                Transaction(customer);
            }
        }

        //This is the main transaction logic think of it like "runGame".  This is where the user will be prompted for the desired soda.
        //get payment from the user.
        //grab the desired soda from the inventory.
        //pass payment to the calculate transaction method to finish up the transaction based on the results.
        private void Transaction(Customer customer)
        {
            // 1. walk up to machine
            // 2. choose a soda from the inventory (call GetSodaFromInventory, use Can object in CalculateTransaction)
            // 3. get money from wallet
            // 4. put money in sodamachine
            // 4. soda machine calculate transaction
            // 5. soda is removed from inventory
            // 6. get change if any
            // 7. put change into wallet
            // 8. put soda into backpack
            string sodaChoice = UserInterface.SodaSelection(_inventory);           
            Can sodaCan = GetSodaFromInventory(sodaChoice);
            
            DepositCoinsIntoRegister(customer.GatherCoinsFromWallet(sodaCan));
            CalculateTransaction(customer.coinsForPayment, sodaCan, customer);
            
            
            //Call the GetSodaFromInventory method.  When you do that, what value is passed with it?

            //other methods to call to make a transaction
            //what are the different types of transactions?
            //what methods would we call in which order for each one of these transaction types?

        }
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda)
        {
            //what is our return type?
            //what methods exist to locate/get a selected can?  userInterface?
            //what values does it need?
            Can getSoda = null;

            for (int i = 0; i < _inventory.Count; i++)
            {

                if (nameOfSoda == _inventory[i].Name)
                {
                    getSoda = _inventory[i];
                    //_inventory.Remove(getSoda);
                    //once its found, dont loop anymore
                    break;
                }
                

            }

            return getSoda;

        }

        //This is the main method for calculating the result of the transaction.
        //It takes in the payment from the customer, the soda object they selected, and the customer who is purchasing the soda.
        //This is the method that will determine the following:
        //If the payment is greater than the price of the soda, and if the sodamachine has enough change to return: Dispense soda, and change to the customer.
        //If the payment is greater than the cost of the soda, but the machine does not have ample change: Dispense payment back to the customer.
        //If the payment is exact to the cost of the soda:  Dispense soda.
        //If the payment does not meet the cost of the soda: dispense payment back to the customer.
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)
        {
            double valueOfCoinList = TotalCoinValue(payment);

            if (valueOfCoinList > chosenSoda.Price)
            {
                if (_register.Count > 0)
                {
                    
                    _inventory.Remove(chosenSoda);
                    customer.AddCoinsIntoWallet(GatherChange(DetermineChange(valueOfCoinList, chosenSoda.Price)));
                    customer.AddCanToBackpack(chosenSoda);
                    
                }
                else if (_register.Count <= 0)
                {
                    customer.AddCoinsIntoWallet(GatherChange(valueOfCoinList));
                }
            }
            else if (valueOfCoinList == chosenSoda.Price)
            {
                _inventory.Remove(chosenSoda);
                customer.AddCanToBackpack(chosenSoda);
            }
            else
            {             
               customer.AddCoinsIntoWallet(GatherChange(valueOfCoinList));
            }
        }
        //Takes in the value of the amount of change needed.
        //Attempts to gather all the required coins from the sodamachine's register to make change.
        //Returns the list of coins as change to despense.
        //If the change cannot be made, return null.
        private List<Coin> GatherChange(double changeValue) //already have change value passed in; need to find coins to equal change value
        {
            changeToDispense = new List<Coin>();


            for (int i = 0; i < changeValue; i++) //loop through changevalue amount
            {
                if (changeValue <= _register.Count) //if register is greater than changevalue then get coin from register
                {
                    changeToDispense.Add(GetCoinFromRegister(_register[i].Name));
                }
                else
                {
                    return null;
                }

            }

            return changeToDispense;

            //what is the change value passed in?  if $1.10
            //what coins are needed to return change value?
            // where are the values of coins stores? in the register list



        }
        //Reusable method to check if the register has a coin of that name.
        //If it does have one, return true.  Else, false.
        private bool RegisterHasCoin(string name)
        {
            bool hasCoin = true;

            foreach (Coin coin in _register)
            {
                if (coin.Name.Contains(name))
                {
                    hasCoin = true;
                    break;
                }
                else
                {
                    hasCoin = false;
                }

            }

            return hasCoin;

        }
        //Reusable method to return a coin from the register.
        //Returns null if no coin can be found of that name.
        private Coin GetCoinFromRegister(string name)
        {
            Coin coinToRemove = null;

            for (int i = 0; i < _register.Count; i++)
            {

                //if (name == _register[i].Name)
                if (RegisterHasCoin(name))
                {
                    coinToRemove = _register[i];
                    _register.Remove(coinToRemove);
                    //once its found, dont loop anymore
                    break;
                }
                else
                {
                    return null;
                }

            }

            return coinToRemove;
        }
        //Takes in the total payment amount and the price of can to return the change amount.
        private double DetermineChange(double totalPayment, double canPrice)
        {
            double changeToReturn;

            changeToReturn = totalPayment - canPrice;

            return changeToReturn;


        }
        //Takes in a list of coins to return the total value of the coins as a double.
        private double TotalCoinValue(List<Coin> payment)
        {
            //what is the value of the coins?
            double valueOfCoinList = 0;
            double value;

            for (int i = 0; i < payment.Count; i++)
            {
                value = _register[i].Value;
                valueOfCoinList = +value;
            }
            //list of coins
            //return value of coins

            return valueOfCoinList;

        }
        //Puts a list of coins into the soda machines register from the customer
        private void DepositCoinsIntoRegister(List<Coin> coins)
        {
            //get coins list from customer wallet
            // add coins list to soda machine register

            for (int i = 0; i < coins.Count; i++)
            {
                _register.Add(coins[i]);
            }




            


        }
    }
}
