using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.MoceanApi.Domain;

namespace Nop.Plugin.Misc.MoceanApi.Data
{
    public class MoceanApiHistoryBuilder : NopEntityBuilder<MoceanApiHistory>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(MoceanApiHistory.Sender))
                .AsString(255)
                .WithColumn(nameof(MoceanApiHistory.Date))
                .AsString(255)
                .WithColumn(nameof(MoceanApiHistory.Message))
                .AsString(255)
                .WithColumn(nameof(MoceanApiHistory.Recipient))
                .AsString(15)
                .WithColumn(nameof(MoceanApiHistory.Response))
                .AsString(255)
                .WithColumn(nameof(MoceanApiHistory.Status))
                .AsString(10)
                .Nullable();
        }
    }
}