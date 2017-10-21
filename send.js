var Nexmo = require('nexmo');

var nexmo = new Nexmo({
  apiKey: '598d80ad',
  apiSecret: '8e435c3198c2641c',
});

var from = 'Mo';
var to = '447870966475';
var text = 'Hello Shakeel';

nexmo.message.sendSms(from, to, text);