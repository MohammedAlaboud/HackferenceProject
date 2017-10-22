var express = require('express');
var fs = require('fs');

fs.writeFile("number.txt", '');

let app = express();

var Nexmo = require("nexmo");
var nexmo = new Nexmo({
  apiKey: '598d80ad',
  apiSecret: '8e435c3198c2641c', 
});


var people = {};
var count  = 0;
var teamHasCpt = [false, false];
var phase = 'assigning-teams';

var events = [];

var from = 'THE GAME';

app.get('/propbloat', (req, res) => {
	res.send(
`
<style>body { margin-left: 300px }
h2 {font-size: 30px }</style>
<button id=resetEl>reset</button>
<button id=phaseEl>change phase</button>
<h1>+44 7520619224</h1>
<h2>flash
<h2>slow
<h2>fast
<h2>cats
<h2>dogs
<h2>troll

<script>
resetEl.onclick = () => {
	const xhr = new XMLHttpRequest;
	xhr.open('GET', '/propbloat2');
	xhr.send();
}
phaseEl.onclick = () => {
	const xhr = new XMLHttpRequest;
	xhr.open('GET', '/propbloat3');
	xhr.send();
}
</script>
`)
});

app.get('/propbloat2', (req, res) => {
	people = {};
	events = {};
	teamHasCpt = [false, false];
	phase = 'assigning-teams';
});

app.get('/propbloat3', (req, res) => {
	phase = 'playing';
});

app.get('/incoming-sms', (req, res) => {

//	req.query.msisdn = Math.random() * 1000000 |0;

	switch(phase) {

		case 'assigning-teams':

			console.log("ASSIGN PHASE")

			var sentText = ''

			if(!people[req.query.msisdn]) {
				var playerTeam = count++ % 2;

				person = {
					team: playerTeam,
					timestamp: Date.now() / 1000 |0
				};

				if(!teamHasCpt[playerTeam]) {
					person.isCpt = true;
					teamHasCpt[playerTeam] = true;

          sentText = person.team === 0 ?
              'YOU ARE BLUE TEAM CAPTAIN! COME UP NOW!!!'
            : 'YOU ARE RED TEAM CAPTAIN! COME UP NOW!!!'

				} else {

          sentText = person.team === 0 ?
              'YOU ARE IN BLUE TEAM'
						: 'YOU ARE IN RED TEAM'

				}

				to = req.query.msisdn;
				nexmo.message.sendSms(from, to, sentText);
			
				people[req.query.msisdn] = person;

				console.log(req.query.msisdn);

			}
      break;

		 case 'playing':


		 console.log("PHASE CHANGE")

		 	if(people[req.query.msisdn]) {
		 		var person = people[req.query.msisdn];

		 		events.push({
		 			team: person.team,
		 			type: req.query.keyword, 
          timestamp: Date.now() / 1000 |0
		 		});

        fs.writeFile("number.txt", JSON.stringify({ eventList: events }));
		 	}

		 console.log(req.query.keyword);

		 	break;
    }

	  res.sendStatus(200)

})

app.get('/data', (req, res) => {

	res.json({text});
})

app.listen(8000, (err) => { console.log(err || 'success')})
