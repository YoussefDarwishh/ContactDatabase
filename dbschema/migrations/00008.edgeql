CREATE MIGRATION m1pj7jodzpandm7e6l4fencknjjfvf2am7oe56e2k26wzoshqm7xwa
    ONTO m1g4y2hs6zzmbxjqcgkb3hd2652vkq5jcb5ftmkibs3vzhinsqdoya
{
  CREATE TYPE default::Contact {
      CREATE REQUIRED PROPERTY date_of_birth: cal::local_date;
      CREATE REQUIRED PROPERTY description: std::str;
      CREATE REQUIRED PROPERTY email: std::str;
      CREATE REQUIRED PROPERTY first_name: std::str;
      CREATE REQUIRED PROPERTY last_name: std::str;
      CREATE REQUIRED PROPERTY marriage_status: std::bool;
      CREATE REQUIRED PROPERTY password: std::str;
      CREATE REQUIRED PROPERTY role: std::str;
      CREATE REQUIRED PROPERTY title: std::str;
      CREATE REQUIRED PROPERTY username: std::str;
  };
};
