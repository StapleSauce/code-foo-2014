
var rows = 20;
var columns = 20;

var coordinatesArray;
//rows
coordinatesArray = new Array(rows);

//allows the coordinates array to be copied
Array.prototype.clone = function() {
   var coordinatesArray = this.slice(0);
   for( var i = 0; i < this.length; i++ ) {
        if( this[i].clone ) {
            //recursion
            coordinatesArray[i] = this[i].clone();
        }
    }
    return coordinatesArray;
}

function drawGrid(coordinatesArray) {
	
	var display = "";
	
	display += "<table>";
	
	for (rowCount = 0; rowCount < 20; rowCount ++) {
		display += '<tr>';
		//loop through each column (horizontal check)
		for (columnCount = 0; columnCount < 20; columnCount ++) {
			display += '<td>' + coordinatesArray[rowCount][columnCount] + '</td>';
			//console.log(coordinatesArray[rowCount][columnCount]);
		}
		
		display += "</tr>";
	}
	
	display += "</table>";
	
	document.getElementById("cross-word").innerHTML = display;
}

function calcGrid(word, direction) {
	/*
	============================================================
	loop through rows
	loop through columns
		if the tempcoordinates are blank
			loop through word
				tempcoordinates position = character at position
				word = character at position
		else
			unset coordinates break loop at this position
			
			if word is complete
				set current coordinates to temp coordinates
	============================================================
	*/
	
	var tempCoordinatesArray = new Array();
	var continueSearch = true;
	var i = 0;
	word = word.toUpperCase();
	var tempWord = "";
	
	tempCoordinatesArray = coordinatesArray.clone();
	
	for (rowCount = 0; rowCount < 20; rowCount ++) {	
		//loop through each column (horizontal check)
		for (columnCount = 0; columnCount < 20; columnCount ++) {
			//if (tempCoordinatesArray[rowCount][columnCount] == "") {
			
				tempColumnCount = columnCount;
				tempRowCount = rowCount;
					
				while (continueSearch) {
					
					if ((tempCoordinatesArray[tempRowCount][tempColumnCount] == word[i]) || ((tempCoordinatesArray[tempRowCount][tempColumnCount] == "") && (tempRowCount < 20) && (tempColumnCount < 20))) {
						tempCoordinatesArray[tempRowCount][tempColumnCount] = word[i];
						tempWord += word[i]
						switch(direction) {
							case "across":
								tempColumnCount ++;
								break;
							case "down":
								tempRowCount ++;
								break;
						}
						i++;
					} else {
						//reset temp coordinates
						tempCoordinatesArray = coordinatesArray.clone();
						continueSearch = false;
					}
					if(tempWord == word) {
						coordinatesArray = tempCoordinatesArray;
						columnCount = 20;
						rowCount = 20;
						continueSearch = false
					}
				}
			//}
			
			i = 0;
			tempWord = "";
			continueSearch = true;
		}
	}
	
	console.log(coordinatesArray);
	drawGrid(coordinatesArray);

}

//initialize the grid
function initGrid() {
	
	for (var rowCount = 0; rowCount < rows; rowCount ++) {
		//this effectively makes the wordsearch act as a grid
		coordinatesArray[rowCount] = new Array(columns);
		//populate the puzzle and coordinates arrays
		for (var columnCount = 0; columnCount < columns; columnCount ++)
		{
			coordinatesArray[rowCount][columnCount] = "";
		}
	}
}

//loop through the word list
function wordList() {
	//for loop should be able to adapt this from my crossword solver
	var wordList = "";
	var wordArray = new Array();
	var direction = "across";
	
	wordList = document.getElementById("wordList").value;
	wordArray = wordList.split("\n");
	
	initGrid();
	
	//wordArray = ["Health", "Score", "Zerg", "Assassin", "Reload", "Inky", "Pylon", "Stryder", "Level", "Bazooka", "Blunderbuss", "Killtacular", "Yellowbrate", "Heist", "Duck", "Huckleberry", "Halo", "LosSantos", "Flappy", "Mushroom", "Atrus", "Horde", "Roivas", "Ganondorf", "KingGraham", "Protoman", "Hydralisk", "Shepard", "NukaCola", "Plasmid", "Wouldyoukindly", "Ellie", "Metroid", "XinZhao", "Zork"];
	
	wordArray.sort(function(a, b){
	  return b.length - a.length; // ASC -> a - b; DESC -> b - a
	});

	for ( wordCount = 0; wordCount < wordArray.length; wordCount ++) {
		
		if (wordCount % 2) {
			direction = "across";
		} else {
			direction = "down";
		}
		calcGrid(wordArray[wordCount], direction);
	}
}