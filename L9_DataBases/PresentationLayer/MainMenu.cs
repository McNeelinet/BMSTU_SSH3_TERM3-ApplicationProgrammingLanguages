using System.Globalization;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using PresentationLayer.Models;

namespace PresentationLayer;

public class MainMenu
{
    private readonly IHospitalService _hospitalService = new HospitalService();
    
    public void EndlessMenu()
    {
        var flag = true;
        while (flag)
        {
            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("ADD) Добавить запись в базу данных");
            Console.WriteLine("EDIT) Редактировать запись из базы данных");
            Console.WriteLine("DELETE) Удалить запись из базы данных");
            Console.WriteLine("DocBySpecId) Получить количество докторов по id спеицальности");
            Console.WriteLine("SpecNameByCertId) Получить название специальности по id сертификата");
            Console.WriteLine("LastCert) Получить последний выданный сертификат");
            Console.WriteLine("QUIT) Просто выйти хочу");

            var line = Console.ReadLine();
            switch (line?.ToLower())
            {
                case "add":
                    switch (SelectTableDialog())
                    {
                        case "Specializations":
                            AddSpecializationDialog();
                            break;
                        case "Doctors":
                            AddDoctorDialog();
                            break;
                        case "Certificates":
                            AddCertificateDialog();
                            break;
                    }
                    break;
                case "edit":
                    switch (SelectTableDialog())
                    {
                        case "Specializations":
                            EditSpecializationDialog();
                            break;
                        case "Doctors":
                            EditDoctorDialog();
                            break;
                        case "Certificates":
                            EditCertificateDialog();
                            break;
                    }
                    break;
                case "delete":
                    switch (SelectTableDialog())
                    {
                        case "Specializations":
                            DeleteSpecializationDialog();
                            break;
                        case "Doctors":
                            DeleteDoctorDialog();
                            break;
                        case "Certificates":
                            DeleteCertificateDialog();
                            break;
                    }
                    break;
                case "docbyspecid":
                    GetDoctorsCountBySpecializationIdDialog();
                    break;
                case "specnamebycertid":
                    GetSpecializationNameByCertificateIdDialog();
                    break;
                case "lastcert":
                    GetLastCertificateDialog();
                    break;
                case "quit":
                    Console.WriteLine("Пока!");
                    flag = false;
                    break;
                default:
                    Console.WriteLine("Введено невалидное значение. Попробуйте еще раз");
                    break;
            }
        }
    }

    private static string SelectTableDialog()
    {
        var tableNames = new List<string>
        {
            "Specializations",
            "Doctors",
            "Certificates"
        };
        
        while (true)
        {
            Console.WriteLine("Выберите таблицу:");
            for (var i = 0; i < tableNames.Count; ++i)
            {
                Console.WriteLine($"{i}) {tableNames[i]}");
            }
            
            try
            {
                var tableNum = Convert.ToInt32(Console.ReadLine());
                if (tableNum >= 0 && tableNum < tableNames.Count)
                    return tableNames[tableNum];
                Console.WriteLine("Выберите число из списка");
            }
            catch (Exception)
            {
                Console.WriteLine("Вы ввели не число");
            }
        }
    }
    
    private void DeleteSpecializationDialog()
    {
        var specialization = SelectSpecializationDialog();
        if (specialization == null)
        {
            Console.WriteLine("Список специализаций пуст\n");
            return;
        }
        _hospitalService.DeleteSpecialization(specialization.Id);
        Console.WriteLine("Специализация удалена\n");
    }
    
    private void DeleteDoctorDialog()
    {
        var doctor = SelectDoctorDialog();
        if (doctor == null)
        {
            Console.WriteLine("Список докторов пуст\n");
            return;
        }
        _hospitalService.DeleteDoctor(doctor.Id);
        Console.WriteLine("Доктор удален\n");
    }
    
    private void DeleteCertificateDialog()
    {
        var certificate = SelectCertificateDialog();
        if (certificate == null)
        {
            Console.WriteLine("Список сертификатов пуст\n");
            return;
        }
        _hospitalService.DeleteCertificate(certificate.Id);
        Console.WriteLine("Сертификат удален\n");
    }

    private void AddSpecializationDialog()
    {
        var specialization = new SpecializationViewModel();

        var flag = true;
        while (flag)
        {
            Console.WriteLine("Текущие настройки:");
            specialization.PrintTable();
            
            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("CNAME) Изменить имя");
            Console.WriteLine("ADD) Добавить запись");

            var line = Console.ReadLine();
            switch (line?.ToLower())
            {
                case "cname":
                    specialization.Name = EnterValidStringDialog();
                    break;
                case "add":
                {
                    try
                    {
                        _hospitalService.AddSpecialization(new SpecializationDTO
                        {
                            Name = specialization.Name
                        });
                        flag = false;
                    }
                    catch (DTOValidationException e)
                    {
                        Console.Write(e.Message);
                        Console.WriteLine($" {e.Property}");
                    }
                    break;
                }
                default:
                    Console.WriteLine("Введено невалидное значение. Попробуйте еще раз");
                    break;
            }
        }
        
        Console.WriteLine("Добавил запись\n");
    }
    
    private void AddDoctorDialog()
    {
        var doctor = new DoctorViewModel();

        var flag = true;
        while (flag)
        {
            Console.WriteLine("Текущие настройки:");
            doctor.PrintTable();
            
            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("CSPEC) Изменить id специализации");            
            Console.WriteLine("CNAME) Изменить имя");
            Console.WriteLine("ADD) Добавить запись");

            var line = Console.ReadLine();
            switch (line?.ToLower())
            {
                case "cspec":
                {
                    var specialization = SelectSpecializationDialog();
                    if (specialization is null)
                    {
                        Console.WriteLine("Не получится добавить доктора. Нет информации о специальностях\n");
                        return;
                    }

                    doctor.SpecializationId = specialization.Id;
                    break;
                }
                case "cname":
                    doctor.Name = EnterValidStringDialog();
                    break;
                case "add":
                {
                    try
                    {
                        _hospitalService.AddDoctor(new DoctorDTO
                        {
                            SpecializationId = doctor.SpecializationId,
                            Name = doctor.Name
                        });
                        flag = false;
                    }
                    catch (DTOValidationException e)
                    {
                        Console.Write(e.Message);
                        Console.WriteLine($" {e.Property}");
                    }
                    break;
                }
                default:
                    Console.WriteLine("Введено невалидное значение. Попробуйте еще раз");
                    break;
            }
        }
        
        Console.WriteLine("Добавил запись\n");
    }
    
    private void AddCertificateDialog()
    {
        var certificate = new CertificateViewModel();

        var flag = true;
        while (flag)
        {
            Console.WriteLine("Текущие настройки:");
            certificate.PrintTable();
            
            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("CDOC) Изменить id доктора");            
            Console.WriteLine("CDESC) Изменить описание");
            Console.WriteLine("CDATE) Изменить дату выдачи");
            Console.WriteLine("ADD) Добавить запись");

            var line = Console.ReadLine();
            switch (line?.ToLower())
            {
                case "cdoc":
                {
                    var doctor = SelectDoctorDialog();
                    if (doctor is null)
                    {
                        Console.WriteLine("Не получится добавить сертификат. Нет информации о докторах\n");
                        return;
                    }

                    certificate.DoctorId = doctor.Id;
                    break;
                }
                case "cdesc":
                    certificate.Description = EnterValidStringDialog();
                    break;
                case "cdate":
                    certificate.Date = EnterValidDatetime().ToUniversalTime();
                    break;
                case "add":
                {
                    try
                    {
                        _hospitalService.AddCertificate(new CertificateDTO
                        {
                            DoctorId = certificate.DoctorId,
                            Description = certificate.Description,
                            Date = certificate.Date
                        });
                        flag = false;
                    }
                    catch (DTOValidationException e)
                    {
                        Console.Write(e.Message);
                        Console.WriteLine($" {e.Property}");
                    }
                    break;
                }
                default:
                    Console.WriteLine("Введено невалидное значение. Попробуйте еще раз");
                    break;
            }
        }
        
        Console.WriteLine("Добавил запись\n");
    }
    
    private void EditSpecializationDialog()
    {
        var specialization = SelectSpecializationDialog();
        if (specialization == null)
        {
            Console.WriteLine("Список специализаций пуст\n");
            return;
        }

        var flag = true;
        while (flag)
        {
            Console.WriteLine("Текущие настройки:");
            specialization.PrintTable();
            
            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("CNAME) Изменить имя");
            Console.WriteLine("UPDATE) Редактировать запись");

            var line = Console.ReadLine();
            switch (line?.ToLower())
            {
                case "cname":
                    specialization.Name = EnterValidStringDialog();
                    break;
                case "update":
                {
                    try
                    {
                        _hospitalService.UpdateSpecialization(specialization.Id, new SpecializationDTO
                        {
                            Name = specialization.Name
                        });
                        flag = false;
                    }
                    catch (DTOValidationException e)
                    {
                        Console.Write(e.Message);
                        Console.WriteLine($" {e.Property}");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                    break;
                }
                default:
                    Console.WriteLine("Введено невалидное значение. Попробуйте еще раз");
                    break;
            }
        }
        
        Console.WriteLine("Обновил запись\n");
    }
    
    private void EditDoctorDialog()
    {
        var doctor = SelectDoctorDialog();
        if (doctor == null)
        {
            Console.WriteLine("Список докторов пуст\n");
            return;
        }

        var flag = true;
        while (flag)
        {
            Console.WriteLine("Текущие настройки:");
            doctor.PrintTable();
            
            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("CSPEC) Изменить id специализации");            
            Console.WriteLine("CNAME) Изменить имя");
            Console.WriteLine("UPDATE) Редактировать запись");

            var line = Console.ReadLine();
            switch (line?.ToLower())
            {
                case "cspec":
                {
                    var specialization = SelectSpecializationDialog();
                    if (specialization is null)
                    {
                        Console.WriteLine("Не получится редактировать доктора. Нет информации о специальностях\n");
                        return;
                    }

                    doctor.SpecializationId = specialization.Id;
                    break;
                }
                case "cname":
                    doctor.Name = EnterValidStringDialog();
                    break;
                case "update":
                {
                    try
                    {
                        _hospitalService.UpdateDoctor(doctor.Id, new DoctorDTO
                        {
                            SpecializationId = doctor.SpecializationId,
                            Name = doctor.Name
                        });
                        flag = false;
                    }
                    catch (DTOValidationException e)
                    {
                        Console.Write(e.Message);
                        Console.WriteLine($" {e.Property}");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                    break;
                }
                default:
                    Console.WriteLine("Введено невалидное значение. Попробуйте еще раз");
                    break;
            }
        }
        
        Console.WriteLine("Обновил запись\n");
    }
    
    private void EditCertificateDialog()
    {
        var certificate = SelectCertificateDialog();
        if (certificate is null)
        {
            Console.WriteLine("Список сертификатов пуст\n");
            return;
        }

        var flag = true;
        while (flag)
        {
            Console.WriteLine("Текущие настройки:");
            certificate.PrintTable();
            
            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("CDOC) Изменить id доктора");            
            Console.WriteLine("CDESC) Изменить описание");
            Console.WriteLine("CDATE) Изменить дату выдачи");
            Console.WriteLine("UPDATE) Редактировать запись");

            var line = Console.ReadLine();
            switch (line?.ToLower())
            {
                case "cdoc":
                {
                    var doctor = SelectDoctorDialog();
                    if (doctor is null)
                    {
                        Console.WriteLine("Не получится редактировать сертификат. Нет информации о докторах\n");
                        return;
                    }

                    certificate.DoctorId = doctor.Id;
                    break;
                }
                case "cdesc":
                    certificate.Description = EnterValidStringDialog();
                    break;
                case "cdate":
                    certificate.Date = EnterValidDatetime().ToUniversalTime();
                    break;
                case "update":
                {
                    try
                    {
                        _hospitalService.UpdateCertificate(certificate.Id, new CertificateDTO
                        {
                            DoctorId = certificate.DoctorId,
                            Description = certificate.Description,
                            Date = certificate.Date
                        });
                        flag = false;
                    }
                    catch (DTOValidationException e)
                    {
                        Console.Write(e.Message);
                        Console.WriteLine($" {e.Property}");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                    break;
                }
                default:
                    Console.WriteLine("Введено невалидное значение. Попробуйте еще раз");
                    break;
            }
        }
        
        Console.WriteLine("Обновил запись\n");
    }

    private void GetDoctorsCountBySpecializationIdDialog()
    {
        var specialization = SelectSpecializationDialog();
        if (specialization == null)
        {
            Console.WriteLine("Список специализаций пуст\n");
            return;
        }

        var docCount = _hospitalService.GetDoctorsCountBySpecializationId(specialization.Id);
        Console.WriteLine($"{docCount} доктор(ов) по специальности {specialization.Name} (id {specialization.Id})\n");
    }

    private void GetSpecializationNameByCertificateIdDialog()
    {
        var certificate = SelectCertificateDialog();
        if (certificate is null)
        {
            Console.WriteLine("Список сертификатов пуст\n");
            return;
        }

        try
        {
            var specName = _hospitalService.GetSpecializationNameByCertificateId(certificate.Id);
            Console.WriteLine($"Сертификат с id {certificate.Id} был выдан доктору со специальностью {specName}");
        }
        catch (DTOValidationException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void GetLastCertificateDialog()
    {
        try
        {
            var certificateDto = _hospitalService.GetLastCertificate();
            var certificate = new CertificateViewModel
            {
                DoctorId = certificateDto.DoctorId,
                Description = certificateDto.Description,
                Date = certificateDto.Date
            };
            certificate.PrintTable();
            Console.WriteLine();
        }
        catch (DTOValidationException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private SpecializationViewModel? SelectSpecializationDialog()
    {
        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SpecializationDTO, SpecializationViewModel>()).CreateMapper();
        var specializations = mapper.Map<IEnumerable<SpecializationDTO>, List<SpecializationViewModel>>(_hospitalService.GetSpecializations());
        if (specializations.Count == 0)
            return null;

        while (true)
        {
            Console.WriteLine("Выберите специализацию:");
            for (var i = 0; i < specializations.Count; ++i)
            {
                Console.WriteLine($"{i}) {specializations[i].Name} (id: {specializations[i].Id})");
            }

            try
            {
                var cpcNumber = Convert.ToInt32(Console.ReadLine());
                if (cpcNumber>= 0 && cpcNumber < specializations.Count)
                    return specializations[cpcNumber];
                Console.WriteLine("Выберите число из списка");
            }
            catch (Exception)
            {
                Console.WriteLine("Вы ввели не число");
            }
        }
    }
    
    private DoctorViewModel? SelectDoctorDialog()
    {
        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DoctorDTO, DoctorViewModel>()).CreateMapper();
        var doctors = mapper.Map<IEnumerable<DoctorDTO>, List<DoctorViewModel>>(_hospitalService.GetDoctors());
        if (doctors.Count == 0)
            return null;

        while (true)
        {
            Console.WriteLine("Выберите доктора:");
            for (var i = 0; i < doctors.Count; ++i)
            {
                Console.WriteLine($"{i}) {doctors[i].Name} (id: {doctors[i].Id})");
            }

            try
            {
                var docNumber = Convert.ToInt32(Console.ReadLine());
                if (docNumber>= 0 && docNumber < doctors.Count)
                    return doctors[docNumber];
                Console.WriteLine("Выберите число из списка");
            }
            catch (Exception)
            {
                Console.WriteLine("Вы ввели не число");
            }
        }
    }
    
    private CertificateViewModel? SelectCertificateDialog()
    {
        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CertificateDTO, CertificateViewModel>()).CreateMapper();
        var certificates = mapper.Map<IEnumerable<CertificateDTO>, List<CertificateViewModel>>(_hospitalService.GetCertificates());
        if (certificates.Count == 0)
            return null;

        while (true)
        {
            Console.WriteLine("Выберите сертификат:");
            for (var i = 0; i < certificates.Count; ++i)
            {
                Console.WriteLine($"{i}) {certificates[i].Description} (id: {certificates[i].Id})");
            }

            try
            {
                var crtNumber = Convert.ToInt32(Console.ReadLine());
                if (crtNumber>= 0 && crtNumber < certificates.Count)
                    return certificates[crtNumber];
                Console.WriteLine("Выберите число из списка");
            }
            catch (Exception)
            {
                Console.WriteLine("Вы ввели не число");
            }
        }
    }
    
    private static string EnterValidStringDialog()
    {
        Console.Write("Введите строку: ");
        var str = Console.ReadLine();
        
        while (string.IsNullOrEmpty(str))
        {
            Console.WriteLine("Строка не должна иметь нулевую длину");
            Console.Write("Введите строку: ");
            str = Console.ReadLine();
        }

        return str;
    }
    
    private DateTime EnterValidDatetime()
    {
        DateTime dt;
        
        while (true)
        {
            Console.WriteLine("Введи дату в таком формате: дд-мм-гггг");
            try
            {
                dt = DateTime.ParseExact(Console.ReadLine() ?? string.Empty, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                return dt;
            }
            catch (FormatException)
            {
                Console.WriteLine("Неправильный формат. Попробуй еще");
            }
        }
    }
}