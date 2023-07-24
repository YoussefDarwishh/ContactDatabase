CREATE MIGRATION m1qdi4ejl5zn5maqiin5big2p7gqfnhcfyj7olu6qffrjoaibcsj7a
    ONTO m15riwaurir2hpckym66y7cewpxs6qrxv6i56lbduba45qjszaogsq
{
  ALTER TYPE default::Contact {
      ALTER PROPERTY BirthDate {
          RENAME TO date_of_birth;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY Description {
          RENAME TO description;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY Email {
          RENAME TO email;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY FirstName {
          RENAME TO first_name;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY LastName {
          RENAME TO last_name;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY MarriageStatus {
          RENAME TO marriage_status;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY Title {
          RENAME TO title;
      };
  };
};
