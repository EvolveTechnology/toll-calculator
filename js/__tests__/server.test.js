import * as server from '../server';
import * as database from '../database';

let db = database;
server.setDb(database);

beforeEach(() => {
  db.seed();
});

it.only('Works', () =>{
  let queue = db.all('queue');
  queue.forEach(function(passage){
    server.main(passage);
  });

  expect(db.all('transactions').length).toBe(13);
});