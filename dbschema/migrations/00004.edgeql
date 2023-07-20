CREATE MIGRATION m15riwaurir2hpckym66y7cewpxs6qrxv6i56lbduba45qjszaogsq
    ONTO m1smuokynexmji6drlnixicjofxbx2jmaajahc4dgm5pgkgwzxsc6a
{
  ALTER TYPE default::Contact {
      CREATE REQUIRED PROPERTY BirthDate: std::datetime {
          SET REQUIRED USING (<std::datetime>{});
      };
      DROP PROPERTY DateOfBirth;
  };
};
