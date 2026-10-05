using CompreAqui.Domain.Attributes;
using CompreAqui.Domain.Commands.Inputs;
using CompreAqui.Domain.Commands.Inputs.CustomerCommands;
using CompreAqui.Domain.Commands.Output;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Enums;
using CompreAqui.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompreAqui.Domain.Handlers
{
    public class CustomerHandler : EntityHandler<Customer>, IHandler
    {
        public CustomerHandler(IContext context) : base(context)
        {
        }

        public void AlteraStatusCustomer(List<Customer> listaCustomer_)
        {
            TimeZoneInfo Standard_Time = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            DateTime dataHoraLocal = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Standard_Time);
            var diaDaSemana = dataHoraLocal.DayOfWeek;

            foreach (var item in listaCustomer_)
            {
                switch (diaDaSemana.ToString())
                {
                    case "Monday":
                        {
                            if (item.OpenMonday > item.CloseMonday)
                            {
                                var openPeriodo1 = item.OpenMonday;
                                var closePeriodo1 = new DateTime(2020,1, 1, 23, 59, 59);
                                var openPeriodo2 = new DateTime(2020, 1, 1, 00, 00, 01);
                                var closePeriodo2 = item.CloseMonday;

                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = ((openPeriodo1.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo1.TimeOfDay)
                                        || (openPeriodo2.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo2.TimeOfDay));
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }
                            else
                            {
                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = (item.OpenMonday.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= item.CloseMonday.TimeOfDay);
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }

                            break;
                        }
                    case "Tuesday":
                        {
                            if (item.OpenTuesday > item.CloseTuesday)
                            {
                                var openPeriodo1 = item.OpenTuesday;
                                var closePeriodo1 = new DateTime(2020,1, 1, 23, 59, 59);
                                var openPeriodo2 = new DateTime(2020, 1, 1, 00, 00, 01);
                                var closePeriodo2 = item.CloseTuesday;

                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = ((openPeriodo1.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo1.TimeOfDay)
                                        || (openPeriodo2.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo2.TimeOfDay));
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }
                            else
                            {
                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = (item.OpenTuesday.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= item.CloseTuesday.TimeOfDay);
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }
                            break;
                        }
                    case "Wednesday":
                        {
                            if (item.OpenWednesday > item.CloseWednesday)
                            {
                                var openPeriodo1 = item.OpenWednesday;
                                var closePeriodo1 = new DateTime(2020,1, 1, 23, 59, 59);
                                var openPeriodo2 = new DateTime(2020, 1, 1, 00, 00, 01);
                                var closePeriodo2 = item.CloseWednesday;

                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = ((openPeriodo1.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo1.TimeOfDay)
                                        || (openPeriodo2.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo2.TimeOfDay));
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }
                            else
                            {
                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = (item.OpenWednesday.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= item.CloseWednesday.TimeOfDay);
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }
                            break;
                        }
                    case "Thursday":
                        {
                            if (item.OpenThursday > item.CloseThursday)
                            {
                                var openPeriodo1 = item.OpenThursday;
                                var closePeriodo1 = new DateTime(2020,1, 1, 23, 59, 59);
                                var openPeriodo2 = new DateTime(2020, 1, 1, 00, 00, 01);
                                var closePeriodo2 = item.CloseThursday;

                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = ((openPeriodo1.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo1.TimeOfDay)
                                        || (openPeriodo2.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo2.TimeOfDay));
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }
                            else
                            {
                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = (item.OpenThursday.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= item.CloseThursday.TimeOfDay);
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }
                            break;
                        }
                    case "Friday":
                        {
                            if (item.OpenFriday > item.CloseFriday)
                            {
                                var openPeriodo1 = item.OpenFriday;
                                var closePeriodo1 = new DateTime(2020,1, 1, 23, 59, 59);
                                var openPeriodo2 = new DateTime(2020, 1, 1, 00, 00, 01);
                                var closePeriodo2 = item.CloseFriday;

                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = ((openPeriodo1.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo1.TimeOfDay)
                                        || (openPeriodo2.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo2.TimeOfDay));
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }
                            else
                            {
                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = (item.OpenFriday.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= item.CloseFriday.TimeOfDay);
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }
                            break;
                        }
                    case "Saturday":
                        {
                            if (item.OpenSaturday > item.CloseSaturday)
                            {
                                var openPeriodo1 = item.OpenSaturday;
                                var closePeriodo1 = new DateTime(2020,1, 1, 23, 59, 59);
                                var openPeriodo2 = new DateTime(2020, 1, 1, 00, 00, 01);
                                var closePeriodo2 = item.CloseSaturday;

                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = ((openPeriodo1.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo1.TimeOfDay)
                                        || (openPeriodo2.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo2.TimeOfDay));
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }
                            else
                            {
                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = (item.OpenSaturday.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= item.CloseSaturday.TimeOfDay);
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }
                            break;
                        }
                    case "Sunday":
                        {
                            if (item.OpenSunday > item.CloseSunday)
                            {
                                var openPeriodo1 = item.OpenSunday;
                                var closePeriodo1 = new DateTime(2020,1, 1, 23, 59, 59);
                                var openPeriodo2 = new DateTime(2020, 1, 1, 00, 00, 01);
                                var closePeriodo2 = item.CloseSunday;

                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = ((openPeriodo1.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo1.TimeOfDay)
                                        || (openPeriodo2.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= closePeriodo2.TimeOfDay));
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }
                            else
                            {
                                if (!item.UpdateStatusManualy)
                                {
                                    item.Opened = (item.OpenSunday.TimeOfDay <= dataHoraLocal.TimeOfDay && dataHoraLocal.TimeOfDay <= item.CloseSunday.TimeOfDay);
                                }
                                else
                                {
                                    item.Opened = item.Opened;
                                }
                            }

                            break;
                        }
                }
            }
        }

        public new async Task<ICommandResult> GetAll()
        {
            try
            {
                string sql = $"SELECT * FROM customers order by opened desc;";
                var retorno = await Repository.GetListBySql<Customer>(sql);
                
                AlteraStatusCustomer(retorno);

                ICommandResult resultadoServico = retorno.Count == 0
                    ? new CommandResult((int)EStatus.NoContent, true, "Nenhum cliente encontrado !", null)
                    : new CommandResult((int)EStatus.Ok, true, "Clientes encontrados com sucesso!", retorno);
                return resultadoServico;
            }
            catch
            {
                return new CommandResult((int)EStatus.InternalServerError, false, "Erro ao tentar buscar os clientes!", null);
                //throw new ArgumentException("Erro ao buscar todos os clientes. Mensagem de erro: ", ex.Message);
            }
        }
    }
}
