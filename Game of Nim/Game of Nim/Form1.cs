using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_of_Nim
{
	public partial class GameOfNim : Form
	{
		//global variables to store gamestate, aswell as random
		int[] heaps;
		int heapsCount;
		Random rand = new Random();

		//form initialisation
		public GameOfNim()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//removes placeholder text
			lblPlayerChange1.Text = "";
			lblPlayerChange2.Text = "";
			lblPlayerChange3.Text = "";
			lblPlayerChange4.Text = "";
			lblPlayerChange5.Text = "";
			lblAIChange1.Text = "";
			lblAIChange2.Text = "";
			lblAIChange3.Text = "";
			lblAIChange4.Text = "";
			lblAIChange5.Text = "";
		}

		//function used to create and instantiate new heaps / game
		private void resetHeaps()
		{
			//get random int for number of new heaps
			heapsCount = rand.Next(2, 6);
			//get random int for total items to distribute between all heaps
			int items = rand.Next((heapsCount * 2)+1, 31);
			//set heaps to a new array with size of previosly created random int
			heaps = new int[heapsCount];
			//creates new array with same size as heaps array to store heap weights
			int[] heapWeights = new int[heapsCount];
			//create int to store total weight shared between all heaps
			int totalHeapWeight = 0;
			//loop through all heaps
			for (int i = 0; i < heapsCount; i++)
			{
				//give each heap a random weight between 1 and 5
				heapWeights[i] = rand.Next(1, 5);
				//add random weight to total heap weight
				totalHeapWeight += heapWeights[i];
			}
			//create int used to ensure all items are distributed
			int totalItemsDistributed = 0;
			//loop through all heaps
			for (int i = 0; i < heapsCount; i++)
			{
				//calculate percent value of heap weight compared to total weight
				double value = (double)heapWeights[i] / (double)totalHeapWeight;
				//multiply the percent value with the total items to find the amount of items to add to this heap
				value *= items;
				//round the item count, convert it to int and assign to the heap
				heaps[i] = Int32.Parse(Math.Round(value).ToString());
				//add the items added to the heap to the total items distributed
				totalItemsDistributed += Convert.ToInt32(value);
			}
			//add any and all remaining items to a random heap
			heaps[rand.Next(0, heapsCount)] += items - totalItemsDistributed;
			//enable and update the heaps on the form
			enableHeaps(heapsCount);
			updateHeaps();
			//add new game event to history
			appendHistory("New Game Created");
		}

		//function used to manage heap UI
		private void updateHeaps()
		{
			//set 1st and 2nd heap labels to heap value
			lblHeap1.Text = heaps[0].ToString();
			lblHeap2.Text = heaps[1].ToString();
			//check if the 1st and 2nd heap is empty, if so disable the button to remove from it
			if (heaps[0] <= 0)
				btnHeap1.Enabled = false;
			if (heaps[1] <= 0)
				btnHeap2.Enabled = false;
			//checks if the 3rd heap is in the current game
			if (heapsCount >= 3)
			{
				//sets the 3rd heap label to the heap value
				lblHeap3.Text = heaps[2].ToString();
				//check if the 3rd heap is empty, if so disable the button to remove from it
				if (heaps[2] <= 0)
					btnHeap3.Enabled = false;
			}
			//checks if the 4th heap is in the current game
			if (heapsCount >= 4)
			{
				//sets the 4th heap label to the heap value
				lblHeap4.Text = heaps[3].ToString();
				//check if the 4th heap is empty, if so disable the button to remove from it
				if (heaps[3] <= 0)
					btnHeap4.Enabled = false;
			}
			//checks if the 5th heap is in the current game
			if (heapsCount >= 5)
			{
				//sets the 5th heap label to the heap value
				lblHeap5.Text = heaps[4].ToString();
				//check if the 5th heap is empty, if so disable the button to remove from it
				if (heaps[4] <= 0)
					btnHeap5.Enabled = false;
			}
			
		}

		//function used to initially enable and disable heaps to match new game
		private void enableHeaps(int count)
		{
			//if 1st heap isn't empty enable the 1st heap label and button
			if (heaps[0] > 0)
			{
				lblHeap1.Enabled = true;
				btnHeap1.Enabled = true;
			}
			//if 2nd heap isn't empty enable the 2st heap label and button
			if (heaps[1] > 0)
			{
				lblHeap2.Enabled = true;
				btnHeap2.Enabled = true;
			}
			//if 3rd heap is enabled and isn't empty enable the 3rd heap label and button
			if (count >= 3 && heaps[2] > 0) { btnHeap3.Enabled = true; lblHeap3.Enabled = true; }
			//if 4th heap is enabled and isn't empty enable the 4th heap label and button
			if (count >= 4 && heaps[3] > 0) { btnHeap4.Enabled = true; lblHeap4.Enabled = true; }
			//if 5th heap is enabled and isn't empty enable the 5th heap label and button
			if (count >= 5 && heaps[4] > 0) { btnHeap5.Enabled = true; lblHeap5.Enabled = true; }
			//if the 3rd heap is not enabled disable both its label and button
			if (count < 3) { btnHeap3.Enabled = false; lblHeap3.Enabled = false; }
			//if the 3rd heap is not enabled disable both its label and button
			if (count < 4) { btnHeap4.Enabled = false; lblHeap4.Enabled = false; }
			//if the 3rd heap is not enabled disable both its label and button
			if (count < 5) { btnHeap5.Enabled = false; lblHeap5.Enabled = false; }
		}

		//function used to update player latest change labels
		private void updatePlayerChange(int heap, int amount)
		{
			//empty all the player change heap labels
			lblPlayerChange1.Text = "";
			lblPlayerChange2.Text = "";
			lblPlayerChange3.Text = "";
			lblPlayerChange4.Text = "";
			lblPlayerChange5.Text = "";
			//use a switch case to select which label to update, then set its value to the negative of player input
			switch(heap)
			{
				//-1 case used to reset all the labels
				case -1:
					break;
				case 0:
					lblPlayerChange1.Text = "-" + amount.ToString();
					break;
				case 1:
					lblPlayerChange2.Text = "-" + amount.ToString();
					break;
				case 2:
					lblPlayerChange3.Text = "-" + amount.ToString();
					break;
				case 3:
					lblPlayerChange4.Text = "-" + amount.ToString();
					break;
				case 4:
					lblPlayerChange5.Text = "-" + amount.ToString();
					break;
			}
		}

		//function used to update AI latest change labels
		private void updateAIChange(int heap, int amount)
		{
			//empty all the AI change heap labels
			lblAIChange1.Text = "";
			lblAIChange2.Text = "";
			lblAIChange3.Text = "";
			lblAIChange4.Text = "";
			lblAIChange5.Text = "";
			//use a switch case to select which label to update, then set its value to the negative of AI input
			switch (heap)
			{
				//-1 case used to reset all the labels
				case -1:
					break;
				case 0:
					lblAIChange1.Text = "-" + amount.ToString();
					break;
				case 1:
					lblAIChange2.Text = "-" + amount.ToString();
					break;
				case 2:
					lblAIChange3.Text = "-" + amount.ToString();
					break;
				case 3:
					lblAIChange4.Text = "-" + amount.ToString();
					break;
				case 4:
					lblAIChange5.Text = "-" + amount.ToString();
					break;
			}
		}

		//these 5 functions are all used for player input, they call the take turn function with the heap they are respective to
		private void button1_Click(object sender, EventArgs e)
		{
			takeTurn(0);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			takeTurn(1);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			takeTurn(2);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			takeTurn(3);
		}

		private void button5_Click(object sender, EventArgs e)
		{
			takeTurn(4);
		}

		//take turn function used to call player input then follow up with AI turn then end the game if conditions are met
		private void takeTurn(int heap)
		{
			//int amount is taken from user text box input to find out how much they wish to remove
			int amount = Convert.ToInt32(txtBoxAmount.Text);
			//add player move event to history
			appendHistory(String.Format("P: {0} items from heap {1}", amount, heap + 1));
			//call remove items based on player input
			removeItems(heap, amount);
			//call update player amount to update the labels to represent the players latest move
			updatePlayerChange(heap, amount);
			//create int of how many heaps are empty then calculate how many are
			int emptyHeaps = 0;
			for (int i = 0; i < heapsCount; i++)
			{
				if (heaps[i] <= 0)
					emptyHeaps++;
			}
			//if all heaps are empty, end game
			if (emptyHeaps == heapsCount)
			{
				//calls end game with true meaning the player won
				endGame(true);
			}
			else
			{
				//if the player does not win
				//have the AI make its move
				AIturn();
				emptyHeaps = 0;
				for (int i = 0; i < heapsCount; i++)
				{
					if (heaps[i] <= 0)
						emptyHeaps++;
				}
				//if all heaps are empty, end game
				if (emptyHeaps == heapsCount)
				{
					//call end game with false as AI wins
					endGame(false);
				}
			}
		}

		//function used to remove items from a heap
		private void removeItems(int heap, int amount)
		{
			//if input from player or AI is less then 1, make it 1
			if (amount <= 1) { amount = 1; }
			//subtract amount from heap
			heaps[heap] -= amount;
			//if heap is now less then 0, set it to 0
			if (heaps[heap] < 0)
			{
				heaps[heap] = 0;
			}
			//update the heaps UI
			updateHeaps();
		}

		//function used for the AI to make a move
		private void AIturn()
		{
			//create a string array used to store all heaps in a binary bitstring format, i.e. 6 = 000110
			string[] nimValues = new string[heapsCount];
			//calculate the bitstrings of all heaps and add to the nimValues array
			for (int i = 0; i < heapsCount; i++)
				nimValues[i] = convertNimValue(heaps[i]);
			//calculate the nimValue of the entire board
			string nimValue = calculateNimValue(nimValues);
			//create int to store amount to change
			int amountToChange;
			//crate int to store heap to change 
			int heapToChange;
			//find and store the interger index of the first 1 bit found in the nimValue
			int firstOdd = nimValue.IndexOf("1");
			//if the nimValue is nothing but 0's then there is no move that guarantees victory, as such the AI will move randomly
			if (firstOdd < 0 || firstOdd > 5)
			{
				//creates random values to move
				heapToChange = rand.Next(0, heapsCount);
				amountToChange = rand.Next(1, heaps[heapToChange]);
			}
			else //there is a move that guarantees victory so the AI will find it
			{
				//create int to represent which Heap to Change
				heapToChange = 0;
				//loop through all heaps until valid heap to change is found
				//valid heap is one where the firstOdd index calculated earlier results in a 1 in its respective nimValue
				for (int i = 0; i < heapsCount; i++)
					if (nimValues[i][firstOdd] == '1')
					{
						//valid is found so set heap to change to i and end the loop
						heapToChange = i;
						break;
					}
				//create string to store bitstring that represents the new value that the heap needs to be set to
				string bitStringamountToChangeTo = "";
				//loop through all values in the bitstrings of the total nimValue and the nimValue of the heapToChange, if when added together the values are even, add 0 if off add 1
				for (int i = 0; i < 6; i++)
				{
					bitStringamountToChangeTo += ((Convert.ToInt32(nimValue[i]) + Convert.ToInt32(nimValues[heapToChange][i])) % 2);
				}
				//create int to represent the integer value of the bitString amount to change to aswell as a count array used in the loop
				int amountToChangeTo = 0;
				int count = 0;
				//loops through integer values 32, 16, 8, 4, 2, 1 and compares then to the 6 bits in the bitStringAmountToChangeTo, if its 1 then add the value of i to amountToChangeTo int
				for (int i = 32; i > 0; i /= 2)
				{
					if (bitStringamountToChangeTo[count] == '1')
						amountToChangeTo += i;
					count++;
				}
				//calculate the change needed to change the heap value to the amountToChangeTo
				amountToChange = heaps[heapToChange] - amountToChangeTo;
			}
			//call the actual move
			removeItems(heapToChange, amountToChange);
			//add its turn to the event history
			appendHistory(String.Format("A: {0} items from heap {1}", amountToChange, heapToChange + 1));
			//and change the AI latest move UI
			updateAIChange(heapToChange, amountToChange);
		}
		
		//this function is used to easily add an entry to the event history, it also remove any events that are older then 11 events to make sure all events fit within the box
		private void appendHistory(string text)
		{
			lstBoxHistory.Items.Add(text);
			while(lstBoxHistory.Items.Count > 11)
				lstBoxHistory.Items.RemoveAt(0);
		}

		//this function calculates the total nimValue of an array for nimValues
		private string calculateNimValue(string[] nimValues)
		{
			//create string to return
			string returnString = "";
			//loop through all 6 values in a bitString
			for (int i = 0; i < 6; i++)
			{
				//n is used to hold the total amount of 1's found at a position in all nimValues
				int n = 0;
				//loop through all the nimValues and add their bits at position i to n
				for (int j = 0; j < heapsCount; j++)
				{
					n += Convert.ToInt32(nimValues[j][i]);
				}
				//find if n is odd then or even, even means 0 odd mean 1	
				n = n % 2;
				//add n to the return bitString
				returnString += n;
			}
			return returnString;
		}

		//function used to turn an int into a bitString
		private string convertNimValue(int value)
		{
			//create string to return
			string returnString = "";
			//loops through all values 32, 16, 8, 4, 2, 1
			for (int i = 32; i >= 1; i /= 2)
			{
				//if the integer value is larger then i
				if (value >= i)
				{
					//add 1 to the return string
					returnString += "1";
					//and substract i from the integer value
					value -= i;
				} 
				else //if not then return 0
					returnString += "0";
			}
			//return the newBitString
			return returnString;
		}

		//function used to inform player of AI victory
		private void endGame(bool player)
		{
			if (player)
			{
				//add Player win game event to history aswell as display its victory via a messageBox 
				appendHistory("Player wins");
				MessageBox.Show("AI Loses, Player Wins");
			}
			else
			{
				//add AI win game event to history aswell as display its victory via a messageBox 
				appendHistory("AI wins");
				MessageBox.Show("Player Loses, AI Wins");
			}
		}

		//text validation for user input
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			//check that new text is valid
			if (System.Text.RegularExpressions.Regex.IsMatch(txtBoxAmount.Text, "[^0-9]"))
			{
				MessageBox.Show("Please enter only integers.");
				txtBoxAmount.Text = txtBoxAmount.Text.Remove(txtBoxAmount.Text.Length - 1);
			}
			if (txtBoxAmount.Text == null)
			{
				txtBoxAmount.Text = "";
			}
			if (txtBoxAmount.Text == "") { txtBoxAmount.Text = "1"; }
			else if (Convert.ToInt32(txtBoxAmount.Text.Trim()) < 1)
			{
				txtBoxAmount.Text = "1";
			}
		}

		//function used to create a new game
		private void btnNewGame_Click(object sender, EventArgs e)
		{
			//reset the heaps
			resetHeaps();
			//disable all heap buttons
			btnHeap1.Enabled = false;
			btnHeap2.Enabled = false;
			btnHeap3.Enabled = false;
			btnHeap4.Enabled = false;
			btnHeap5.Enabled = false;
			//remove text in all latest change labels
			updatePlayerChange(-1, -1);
			updateAIChange(-1, -1);
			//enable start game button
			btnStartGame.Enabled = true;
		}

		//function used to start a new game
		private void btnStartGame_Click(object sender, EventArgs e)
		{
			//add game start event to history
			appendHistory("Game Started");
			if (rand.Next(0, 2) == 0)
			{
				//call AI turn as it moves first
				AIturn();
			}
			//enable the heaps
			enableHeaps(heapsCount);
			//disable start game button
			btnStartGame.Enabled = false;
		}
	}
}
