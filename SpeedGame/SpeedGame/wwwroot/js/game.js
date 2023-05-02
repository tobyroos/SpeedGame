"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();
var Selected;
var OpponentStuck;
var PlayerStuck;
var PlayAgain;
var HandJS;
var OpponentHandCountJS;
var PlayerDrawStackJS;
var OpponentDrawStackCountJS;
var PlayStack1JS;
var PlayStack2JS;
var ExStack1JS;
var ExStack2JS;
var PlayStack1JSTop;
var PlayStack2JSTop;

//Disable the send button until connection is established.
document.getElementById("playButton").disabled = true
document.getElementById("playAgainButton").style.visibility = "hidden";

connection.on("CreateGame", function (playerStack, oppStackCount, playerDrawStack, oppDrawStackCt, playStack1, playStack2, exStack1, exStack2, playStack1Top, playStack2Top) {
    HandJS = JSON.parse(playerStack);
    OpponentHandCountJS = oppStackCount;
    PlayerDrawStackJS = JSON.parse(playerDrawStack);
    OpponentDrawStackCountJS = oppDrawStackCt;
    PlayStack1JS = JSON.parse(playStack1);
    PlayStack2JS = JSON.parse(playStack2);
    ExStack1JS = JSON.parse(exStack1);
    ExStack2JS = JSON.parse(exStack2);
    PlayStack1JSTop = JSON.parse(playStack1Top);
    PlayStack2JSTop = JSON.parse(playStack2Top);
    OpponentStuck = 0;
    PlayerStuck = 0;
    PlayAgain = 0

    document.getElementById("playerStackCt").innerHTML = "Your Stack: " + PlayerDrawStackJS.length + " Cards";
    document.getElementById("opponentStackCt").innerHTML = "Opponent Stack: " + OpponentDrawStackCountJS + " Cards";

    document.getElementById("playStack1").src = PlayStack1JSTop.associatedImg;
    document.getElementById("playStack2").src = PlayStack2JSTop.associatedImg;

    document.getElementById("playerCard1").src = HandJS[0].associatedImg;
    document.getElementById("playerCard2").src = HandJS[1].associatedImg;
    document.getElementById("playerCard3").src = HandJS[2].associatedImg;
    document.getElementById("playerCard4").src = HandJS[3].associatedImg;
    document.getElementById("playerCard5").src = HandJS[4].associatedImg;
    document.getElementById("exStack1").style.display = "inline";
    document.getElementById("exStack2").style.display = "inline";
    document.getElementById("stuckBtn").style.visibility = "visible";
    document.getElementById("playButton").style.display = "none";
    document.getElementById("playAgainButton").style.visibility = "hidden";
    document.getElementById("result").className = "d-none";

});

connection.on("Restart", function (playerStack, oppStackCount, playerDrawStack, oppDrawStackCt, playStack1, playStack2, exStack1, exStack2, playStack1Top, playStack2Top, playAgain) {
    HandJS = JSON.parse(playerStack);
    OpponentHandCountJS = oppStackCount;
    PlayerDrawStackJS = JSON.parse(playerDrawStack);
    OpponentDrawStackCountJS = oppDrawStackCt;
    PlayStack1JS = JSON.parse(playStack1);
    PlayStack2JS = JSON.parse(playStack2);
    ExStack1JS = JSON.parse(exStack1);
    ExStack2JS = JSON.parse(exStack2);
    PlayStack1JSTop = JSON.parse(playStack1Top);
    PlayStack2JSTop = JSON.parse(playStack2Top);
    OpponentStuck = 0;
    PlayerStuck = 0;
    PlayAgain = 0;
    document.getElementById("playStack2").style.display = "inline";
    document.getElementById("playStack1").style.display = "inline";
    document.getElementById("exStack1").style.display = "inline";
    document.getElementById("exStack2").style.display = "inline";
    document.getElementById("playerStackCt").innerHTML = "Your Stack: " + PlayerDrawStackJS.length + " Cards";
    document.getElementById("opponentStackCt").innerHTML = "Opponent Stack: " + OpponentDrawStackCountJS + " Cards";

    document.getElementById("playStack1").src = PlayStack1JSTop.associatedImg;
    document.getElementById("playStack2").src = PlayStack2JSTop.associatedImg;

    document.getElementById("playerCard1").src = HandJS[0].associatedImg;
    document.getElementById("playerCard2").src = HandJS[1].associatedImg;
    document.getElementById("playerCard3").src = HandJS[2].associatedImg;
    document.getElementById("playerCard4").src = HandJS[3].associatedImg;
    document.getElementById("playerCard5").src = HandJS[4].associatedImg;
    document.getElementById("oppCard1").src = "/img/back.png";
    document.getElementById("oppCard2").src = "/img/back.png";
    document.getElementById("oppCard3").src = "/img/back.png";
    document.getElementById("oppCard4").src = "/img/back.png";
    document.getElementById("oppCard5").src = "/img/back.png";
    document.getElementById("oppCard5").src = "/img/back.png";

    document.getElementById("exStack1").style.display = "inline";
    document.getElementById("exStack2").style.display = "inline";
    document.getElementById("stuckBtn").style.visibility = "visible";
    document.getElementById("playButton").style.display = "none";
    document.getElementById("playAgainButton").style.visibility = "hidden";
    document.getElementById("result").className = "d-none";

});

connection.on("UpdateGame", function (playerStack, playerDrawStack, playStack1, exStack1, exStack2, playStack1Top, stackNum, handCount, result) {
    HandJS = JSON.parse(playerStack);
    PlayerDrawStackJS = JSON.parse(playerDrawStack);
    PlayStack1JS = JSON.parse(playStack1);
    ExStack1JS = JSON.parse(exStack1);
    ExStack2JS = JSON.parse(exStack2);

    document.getElementById("playerStackCt").innerHTML = "Your Stack: " + PlayerDrawStackJS.length + " Cards";

    if (stackNum == 1) {
        PlayStack1JSTop = JSON.parse(playStack1Top);
        document.getElementById("playStack1").src = PlayStack1JSTop.associatedImg;
    } else {
        PlayStack2JSTop = JSON.parse(playStack1Top);
        document.getElementById("playStack2").src = PlayStack2JSTop.associatedImg;
    }

    if (handCount > 0) {
        document.getElementById("playerCard1").src = HandJS[0].associatedImg;
    } else {
        document.getElementById("playerCard1").src = "/img/blank.png";
    }
    if (handCount > 1) {
        document.getElementById("playerCard2").src = HandJS[1].associatedImg;
    } else {
        document.getElementById("playerCard2").src = "/img/blank.png";
    }
    if (handCount > 2) {
        document.getElementById("playerCard3").src = HandJS[2].associatedImg;
    } else {
        document.getElementById("playerCard3").src = "/img/blank.png";
    }
    if (handCount > 3) {
        document.getElementById("playerCard4").src = HandJS[3].associatedImg;
    } else {
        document.getElementById("playerCard4").src = "/img/blank.png";
    }
    if (handCount > 4) {
        document.getElementById("playerCard5").src = HandJS[4].associatedImg;
    } else {
        document.getElementById("playerCard5").src = "/img/blank.png";
    }

    if (result == 1) {
        document.getElementById("result").innerHTML = "YOU LOSE";
        document.getElementById("result").className = "d-block";
        document.getElementById("playStack2").style.display = "none";
        document.getElementById("playStack1").style.display = "none";
        document.getElementById("exStack1").style.display = "none";
        document.getElementById("exStack2").style.display = "none";
        document.getElementById("stuckBtn").style.visibility = "hidden";

        document.getElementById("playAgainButton").style.visibility = "visible";
        document.getElementById("playAgainButton").disabled = false;

    } else if (result == 2) {
        document.getElementById("result").innerHTML = "YOU WIN";
        document.getElementById("result").className = "d-block";
        document.getElementById("playStack2").style.display = "none";
        document.getElementById("playStack1").style.display = "none";
        document.getElementById("exStack1").style.display = "none";
        document.getElementById("exStack2").style.display = "none";
        document.getElementById("stuckBtn").style.visibility = "hidden";

        document.getElementById("playAgainButton").style.visibility = "visible";
        document.getElementById("playAgainButton").disabled = false;
    }



    document.getElementById("playButton").style.display = "none";
});

connection.on("UpdateGameOpp", function (oppDrawStackCt, playStack1Top, stackNum, handCount, result) {
    OpponentDrawStackCountJS = oppDrawStackCt;

    document.getElementById("opponentStackCt").innerHTML = "Opponent Stack: " + OpponentDrawStackCountJS + " Cards";

    if (stackNum == 1) {
        PlayStack1JSTop = JSON.parse(playStack1Top);
        document.getElementById("playStack1").src = PlayStack1JSTop.associatedImg;
    } else {
        PlayStack2JSTop = JSON.parse(playStack1Top);
        document.getElementById("playStack2").src = PlayStack2JSTop.associatedImg;
    }
    
    if (handCount > 0) {
        document.getElementById("oppCard1").src = "/img/back.png";
    } else {                     
        document.getElementById("oppCard1").src = "/img/blank.png";
    }                           
    if (handCount > 1) {        
        document.getElementById("oppCard2").src = "/img/back.png";
    } else {                    
        document.getElementById("oppCard2").src = "/img/blank.png";
    }                          
    if (handCount > 2) {       
        document.getElementById("oppCard3").src = "/img/back.png";
    } else {                    
        document.getElementById("oppCard3").src = "/img/blank.png";
    }                           
    if (handCount > 3) {        
        document.getElementById("oppCard4").src = "/img/back.png";
    } else {                     
        document.getElementById("oppCard4").src = "/img/blank.png";
    }                            
    if (handCount > 4) {         
        document.getElementById("oppCard5").src = "/img/back.png";
    } else {
        document.getElementById("oppCard5").src = "/img/blank.png";
    }

    if (result == 1) {
        document.getElementById("result").innerHTML = "YOU LOSE";
        document.getElementById("result").className = "d-block";
        document.getElementById("playStack2").style.display = "none";
        document.getElementById("playStack1").style.display = "none";
        document.getElementById("exStack1").style.display = "none";
        document.getElementById("exStack2").style.display = "none";
        document.getElementById("stuckBtn").style.visibility = "hidden";

        document.getElementById("playAgainButton").style.visibility = "visible";
        document.getElementById("playAgainButton").disabled = false;

    } else if (result == 2) {
        document.getElementById("result").innerHTML = "YOU WIN";
        document.getElementById("result").className = "d-block";
        document.getElementById("playStack2").style.display = "none";
        document.getElementById("playStack1").style.display = "none";
        document.getElementById("exStack1").style.display = "none";
        document.getElementById("exStack2").style.display = "none";
        document.getElementById("stuckBtn").style.visibility = "hidden";

        document.getElementById("playAgainButton").style.visibility = "visible";
        document.getElementById("playAgainButton").disabled = false;
    }

    document.getElementById("playButton").style.display = "none";
});
connection.on("UpdatePlayField", function (playStack1, playStack2, playStack1Top, playStack2Top, exStack1Str, exStack2Str, opponentStuck, playerStuck) {
    PlayStack1JS = JSON.parse(playStack1);
    PlayStack2JS = JSON.parse(playStack2);
    PlayStack1JSTop = JSON.parse(playStack1Top);
    PlayStack2JSTop = JSON.parse(playStack2Top);
    ExStack1JS = JSON.parse(exStack1Str);
    ExStack2JS = JSON.parse(exStack2Str);
    OpponentStuck = opponentStuck;
    PlayerStuck = playerStuck;

    if (PlayerStuck == 1) {
        document.getElementById("stuckBtn").className = "px-3 btn btn-danger";
    } else {
        document.getElementById("stuckBtn").className = "px-3 btn btn-primary";
    }

    document.getElementById("playStack1").src = PlayStack1JSTop.associatedImg;
    document.getElementById("playStack2").src = PlayStack2JSTop.associatedImg;

});

connection.on("PlayAgainChoice", function (playAgain) {
    PlayAgain = playAgain;
});

connection.start().then(function () {
    document.getElementById("playButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("playButton").addEventListener("click", function (event) {
    connection.invoke("InitiateGame").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
document.getElementById("playAgainButton").addEventListener("click", function (event) {
    connection.invoke("RestartGame", PlayAgain).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("playerCard1").addEventListener("click", function (event) {
    Selected = 0;
    document.getElementById("playerCard1").border = 1;
    document.getElementById("playerCard2").border = 0;
    document.getElementById("playerCard3").border = 0;
    document.getElementById("playerCard4").border = 0;
    document.getElementById("playerCard5").border = 0;
})

document.getElementById("playerCard2").addEventListener("click", function (event) {
    Selected = 1;
    document.getElementById("playerCard1").border = 0;
    document.getElementById("playerCard2").border = 1;
    document.getElementById("playerCard3").border = 0;
    document.getElementById("playerCard4").border = 0;
    document.getElementById("playerCard5").border = 0;
})
document.getElementById("playerCard3").addEventListener("click", function (event) {
    Selected = 2;
    document.getElementById("playerCard1").border = 0;
    document.getElementById("playerCard2").border = 0;
    document.getElementById("playerCard3").border = 1;
    document.getElementById("playerCard4").border = 0;
    document.getElementById("playerCard5").border = 0;
})
document.getElementById("playerCard4").addEventListener("click", function (event) {
    Selected = 3;
    document.getElementById("playerCard1").border = 0;
    document.getElementById("playerCard2").border = 0;
    document.getElementById("playerCard3").border = 0;
    document.getElementById("playerCard4").border = 1;
    document.getElementById("playerCard5").border = 0;

})
document.getElementById("playerCard5").addEventListener("click", function (event) {
    Selected = 4;
    document.getElementById("playerCard1").border = 0;
    document.getElementById("playerCard2").border = 0;
    document.getElementById("playerCard3").border = 0;
    document.getElementById("playerCard4").border = 0;
    document.getElementById("playerCard5").border = 1;
})
document.getElementById("playStack1").addEventListener("click", function (event) {
    connection.invoke("compareCard", JSON.stringify(PlayStack1JS), JSON.stringify(HandJS[Selected]), JSON.stringify(HandJS), OpponentHandCountJS, JSON.stringify(PlayerDrawStackJS), OpponentDrawStackCountJS, JSON.stringify(PlayStack1JSTop), JSON.stringify(ExStack1JS), JSON.stringify(ExStack2JS), 1).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
document.getElementById("playStack2").addEventListener("click", function (event) {
    connection.invoke("compareCard", JSON.stringify(PlayStack2JS), JSON.stringify(HandJS[Selected]), JSON.stringify(HandJS), OpponentHandCountJS, JSON.stringify(PlayerDrawStackJS), OpponentDrawStackCountJS, JSON.stringify(PlayStack2JSTop), JSON.stringify(ExStack1JS), JSON.stringify(ExStack2JS), 2).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
document.getElementById("stuckBtn").addEventListener("click", function (event) {

    PlayerStuck = (PlayerStuck + 1) % 2;
    if (PlayerStuck == 1) {
        document.getElementById("stuckBtn").className = "px-3 btn btn-danger";
    } else {
        document.getElementById("stuckBtn").className = "px-3 btn btn-primary";
    }

    connection.invoke("CardFlip", JSON.stringify(ExStack1JS), JSON.stringify(ExStack2JS), JSON.stringify(PlayStack1JS), JSON.stringify(PlayStack2JS), JSON.stringify(PlayStack1JSTop), JSON.stringify(PlayStack2JSTop), OpponentStuck, PlayerStuck).catch(function (err) {
        return console.error(err.toString());
    })
});