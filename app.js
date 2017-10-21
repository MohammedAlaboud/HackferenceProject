var Express = require('express');
var fs = require('fs');

let app = Express();

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
<button id=resetEl>reset</button>
<button id=phaseEl>change phase</button>
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

			var sentText = ''

			if(!people[req.query.msisdn]) {
				var playerTeam = count++ % 2;

				person = {
					team: playerTeam,
					timestamp: Date.now()
				};

				if(!teamHasCpt[playerTeam]) {

					person.isCpt = true;
					teamHasCpt[playerTeam] = false;

					if(person.team === 0){

						sentText = 'YOU ARE BLUE TEAM CAPTAIN! COME UP NOW!!!'

					} else {

						sentText = 'YOU ARE RED TEAM CAPTAIN! COME UP NOW!!!'

					}

				} else {

					if(person.team === 0){
						
						sentText = 'YOU ARE IN BLUE TEAM'

					} else {

						sentText = 'YOU ARE IN RED TEAM'

					}

				}

				to = req.query.msisdn;
				nexmo.message.sendSms(from, to, sentText);
			
				people[req.query.msisdn] = person;

		  		fs.writeFile("number.txt", JSON.stringify(people));

			}

		  	
		  	break;

		 case 'playing':

		 	console.log("REACHED")

		 	if(people[req.query.msisdn]) {
		 		var person = people[req.query.msisdn];

		 		events.push({
		 			team: person.team,
		 			type: req.query.keyword, 
		 		})
		 	}

			fs.writeFile("number.txt", JSON.stringify(events));

		 	break;
	  	}

	  res.sendStatus(200)

})

app.get('/data', (req, res) => {

	res.json({text});
})

app.listen(8000, (err) => { console.log(err || 'success')})