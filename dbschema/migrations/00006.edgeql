CREATE MIGRATION m1ty4xfisd6qfif4acls7pnele3qq4egvno7wzfodtnczk667rvjsq
    ONTO m1qdi4ejl5zn5maqiin5big2p7gqfnhcfyj7olu6qffrjoaibcsj7a
{
  ALTER TYPE default::Contact {
      CREATE REQUIRED PROPERTY password: std::str {
          SET REQUIRED USING (<std::str>{});
      };
      CREATE REQUIRED PROPERTY role: std::str {
          SET REQUIRED USING (<std::str>{});
      };
      CREATE REQUIRED PROPERTY username: std::str {
          SET REQUIRED USING (<std::str>{});
      };
  };
};
