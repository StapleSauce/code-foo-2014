
var wordArrayList = [];
var charArray = {};

function calcGrid() {
	/*
	the class should allow me to reference the cell using jquery in order to place the character
	
		table
		for rows
			tr class row-count
				for columns
					td class column count
					close column
			close row
		end table
	*/
}

function createCharArray() {
	//create array of captial letters
	var a = 65;
	for (var i = 0; i<26; i++) {
		charArray[String.fromCharCode(a + i)] = {"occurance": 0};
	}
	
	processOccurance("AARDVARK");
	processOccurance("ANIMAL");
}
/*
//create the charArray object
//store the character occurance
//and the words that contain this character
charArray["A"] = {"occurance": 3,"words": "aardvark"}
"A":[
		{
			"occurance":[number of times char occurs in word],
			"words":["word1","word1"]
		}
	]
*/
function processOccurance(word) {

	var characterOccurances = 0;
	var words = new Array();
	var position = "";
	var positions = new Array();
	
	//check how many times a character occurs add to the count store the word
	for(var character in charArray) {
		//reset words array
		words = new Array();
		positions = new Array();
		position = "";
		//get the words attached to the current character
		if(charArray[character].words) {
			words = charArray[character].words;
		}
		if(charArray[character].char_pos) {
			positions = charArray[character].char_pos;
		}
		for (var i = 0; i < word.length; i++) {
			//positions = new Array();
			characterOccurances = charArray[character].occurance
			if (word[i] == character) {
				characterOccurances ++;
				position += i + ',';
				//console.log(position);
					
				//check if word is in list
				if(words.indexOf(word) == -1) {
					//add this word and the character's postion to the list
					//positions[word] = i;
					//words.push(positions);
					words.push(word);
				}
			}
			
			charArray[character] = {"occurance": characterOccurances};
		}
		
		if(position != "") {
			positions.push(position);
		}
		
		charArray[character] = {"occurance": characterOccurances, "words": words, "char_pos": positions};
	}
	
	console.log(charArray);
	createIntersect();
}

//get most common occurance
//check words length
//select two random words from a character
//remove word from array
function createIntersect() {

	var words = new Array();
	var word = "";
	var mostFrequentCharacter = "";
	var mostFrequentOccurance = 0;
	var index = 0;
	
	//loop through characters
	for(var character in charArray) {
		//get the words attached to the character
		words = charArray[character].words;
		//if there are more occurances under the current character
		if (mostFrequentOccurance < charArray[character].occurance) {
			//if there are still words attached to that character
			if(words.length) {
				//set most occurances
				mostFrequentOccurance = charArray[character].occurance;
				mostFrequentCharacter = character;
			}
		}
	}
	
	words = charArray[mostFrequentCharacter].words;
	
	var coordinatesArray = new Array();
	//rows
	coordinatesArray[mostFrequentCharacter] = new Array(20);
	//columns
	coordinatesArray[mostFrequentCharacter][20] = new Array(20);
	
	//loop through each row (vertical check)
	for (rowCount = 0; rowCount < 20; rowCount ++) {	
		//loop through each column (horizontal check)
		for (columnCount = 0; columnCount < 20; columnCount ++) {
			
		}
	}
	
	//while index of != -1
	//check if coordinates are still valid
	//if not index of +1 becomes the start position (get next occurance in string)
	//while (word.IndexOf(mostFrequentCharacter) != -1) {
		//console.log(charArray[mostFrequentCharacter]);
		//coordinates[word] = {"coordinates": [1, 1]};
		
	//word = words[0];
	//var coordinatesArray[column] = new Array(columns);
	//var coordinatesArray[row] = new Array(rowss);
	
	//mostFrequentOccurance --;
	//coordinateList.push(coordinates);
	
	//console.log(charArray);
}

//loop through all words in the list
function wordList() {
	//for loop should be able to adapt this from my crossword solver
}

//initialize the charArray
//initialize the grid
//call the word loop
function init() {
	
}