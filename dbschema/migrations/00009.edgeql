CREATE MIGRATION m1mdvgycb5vba3gwn26kui7bekxm2zroomgdh4il5c3um3k5wyw23q
    ONTO m1pj7jodzpandm7e6l4fencknjjfvf2am7oe56e2k26wzoshqm7xwa
{
  ALTER TYPE default::Contact {
      DROP PROPERTY date_of_birth;
      CREATE REQUIRED PROPERTY date_of_birth: std::datetime {
          SET REQUIRED USING (<std::datetime>{});
      };
  };
};
