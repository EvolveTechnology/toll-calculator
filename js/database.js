import * as vehicle from "./vehicle";

let tables = {
  'queue': [],
  'transactions': [],
};

function save(table, data) {
  tables[table].push(data);
}

function all(type) {
  return tables[type];
}

function transactionsById(id) {
  return tables.transactions.filter(transaction => transaction.id === id);
}

function seed() {
  tables.queue = [
    {
      id: 'ACID123',
      vehicleType: vehicle.car.type,
      date: new Date('2018-10-07T06:01Z'),
    }, {
      id: 'BURN987',
      vehicleType: vehicle.car.type,
      date: new Date('2018-10-07T17:33Z'),
    }, {
      id: 'BURN987',
      vehicleType: vehicle.car.type,
      date: new Date('2018-10-07T18:33Z'),
    }, {
      id: 'ACID123',
      vehicleType: vehicle.car.type,
      date: new Date('2018-10-08T06:01Z'),
    }, {
      id: 'BATMAN',
      vehicleType: vehicle.military.type,
      date: new Date('2018-10-08T06:01Z'),
    }, {
      id: 'BURN987',
      vehicleType: vehicle.car.type,
      date: new Date('2018-10-08T06:02Z'),
    },{
      id: 'ACID123',
      vehicleType: vehicle.car.type,
      date: new Date('2018-10-08T06:40Z'),
    }, {
      id: 'ACID123',
      vehicleType: vehicle.car.type,
      date: new Date('2018-10-08T06:42Z'),
    },{
      id: 'BURN987',
      vehicleType: vehicle.car.type,
      date: new Date('2018-10-08T06:45Z'),
    }, {
      id: 'ACID123',
      vehicleType: vehicle.car.type,
      date: new Date('2018-10-08T17:33Z'),
    },
  ];
}

export {
  save,
  all,
  transactionsById,
  seed
};
