const sql = require('mssql');
const parser = require('mssql-connection-string');

class PeopleDbContext {
    constructor(connectionString, log) {
        log("PeopleDbContext object has been created.");
        this.log = log;
        this.config = parser(connectionString);
        this.config.options.port = 1433;
    this.getPeople = this.getPeople.bind(this);
  }

  async getPeople() {
    this.log("getPeople function - run");
    const connection = await new sql.ConnectionPool(this.config).connect();
    const request = new sql.Request(connection);
    const result = await request.query("select * from People");
    this.log("getPeople function - done");
    return result.recordset;
  }

  async addPerson(name, surname, phoneNumber) {
    this.log("addPerson function - run");
    const connection = await new sql.ConnectionPool(this.config).connect();
    const request = new sql.Request(connection);
    const result = await request.query(
      `insert into People (FirstName, LastName, PhoneNumber) values (${name}, ${surname}, ${phoneNumber})`
    );
    this.log("addPerson function - done");
    return result.recordset;
  }

  async delPerson(personId) {
    this.log("delPerson function - run");
    const connection = await new sql.ConnectionPool(this.config).connect();
    const request = new sql.Request(connection);
    const result = await request.query(
      `delete from People where PersonId=${personId}`
    );
    this.log("delPerson function - done");
    return result.recordset;
  }

  async findSPerson(personId) {
    this.log("findPerson function - run");
    const connection = await new sql.ConnectionPool(this.config).connect();
    const request = new sql.Request(connection);
    const result = await request.query(
      `select * from People where PersonId=${personId}`
    );
    this.log("findPerson function - done");
    return result.recordset;
  }
}

module.exports = PeopleDbContext;