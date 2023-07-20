CREATE MIGRATION m1nbiow2ojmjtbymxthq2yswhjx54pjudqmuwddm75kvuiufqkhzoq
    ONTO m1uwekrn4ni4qs7ul7hfar4xemm5kkxlpswolcoyqj3xdhweomwjrq
{
  DROP TYPE default::Movie;
  CREATE TYPE default::Contact {
      CREATE REQUIRED PROPERTY DateOfBirth: cal::local_datetime;
      CREATE REQUIRED PROPERTY Description: std::str;
      CREATE REQUIRED PROPERTY Email: std::str;
      CREATE REQUIRED PROPERTY FirstName: std::str;
      CREATE REQUIRED PROPERTY LastName: std::str;
      CREATE REQUIRED PROPERTY MarriageStatus: std::bool;
      CREATE REQUIRED PROPERTY Title: std::str;
  };
  DROP TYPE default::Person;
};
