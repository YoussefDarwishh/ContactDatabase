CREATE MIGRATION m1smuokynexmji6drlnixicjofxbx2jmaajahc4dgm5pgkgwzxsc6a
    ONTO m1nbiow2ojmjtbymxthq2yswhjx54pjudqmuwddm75kvuiufqkhzoq
{
  ALTER TYPE default::Contact {
      ALTER PROPERTY DateOfBirth {
          SET TYPE std::datetime USING (std::to_datetime(.DateOfBirth, 'UTC'));
      };
  };
};
