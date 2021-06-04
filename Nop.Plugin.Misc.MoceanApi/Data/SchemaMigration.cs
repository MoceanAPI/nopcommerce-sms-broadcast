using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.MoceanApi.Domain;

namespace Nop.Plugin.Misc.MoceanApi.Data
{
    [SkipMigrationOnUpdate]
    [NopMigration("2020/06/05 12:00:00", "Misc.MoceanApi base schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        #region Fields

        protected IMigrationManager _migrationManager;

        #endregion

        #region Ctor

        public SchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            _migrationManager.BuildTable<MoceanApiHistory>(Create);
        }

        #endregion
    }
}
