using System.IO;
using System.Net;
using System.Net.Mime;
using AES.Domain.Course;
using AES.Infrastructure.EntityFrameworkCore;
using AES.Story;

namespace AES.Init
{
    using Domain;
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class dymmuCurriculum
    {
        public string SubjectName { get; set; }
        public string TypeTesting { get; set; }
        public int Semester { get; set; }
    }

    public class Initializer
    {
        public static IDictionary<string, TypeTesting> CreateDictionaryByName(ITypeTestingRepository repository)
        {
            IDictionary<string, TypeTesting> dic = new Dictionary<string, TypeTesting>();
            foreach (var element in repository.GetQuery().ToList())
            {
                dic.Add(element.Name, element);
            }
            return dic;
        }

        public static IDictionary<string, Subject> CreateDictionaryByName(ISubjectRepository repository)
        {
            IDictionary<string, Subject> dic = new Dictionary<string, Subject>();
            foreach (var element in repository.GetQuery().ToList())
            {
                dic.Add(element.Name, element);
            }
            return dic;
        }

        public static Curriculum createCurriculum(Student student, dymmuCurriculum [] curriculaTemplate, IUnitOfWork unitOfWork)
        {
            var subjects = CreateDictionaryByName(unitOfWork.SubjectRepository);
            var typeTesting = CreateDictionaryByName(unitOfWork.TypeTestingRepository);

            var curriculum = new Curriculum()
            {
                Id = student.Id,
                DateOfAppointment = DateTime.UtcNow,
                Student = student,
                tag = "default"
                
            };
            curriculum.DateOfAppointment = DateTime.UtcNow;
            Module module = new SubjectCycle()
            {
                Id = Guid.NewGuid(),
                Grade = null,
                IsRequared = true,
                Title = "default",
                Curriculum = curriculum
            };
            curriculum.Modules.Add(module);
            foreach (var item in curriculaTemplate)
            {
                module.Items.Add(new CurriculumItem()
                {
                    Id = Guid.NewGuid(),
                    Grade = null,
                    IsRequired = true,
                    Module = module,
                    Subject = subjects[item.SubjectName],
                    Semester = item.Semester,
                    TypeTesting = typeTesting[item.TypeTesting]
                });
            }
            return curriculum;
        }

        private static Direction CreateDirection(Guid id, string name, string abbreviation, string shortname)
        {
            var val = new Direction(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddDirectionIfNotExist(IDirectionRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            var val = rep.Get(id);
            if (val == null)
            {
                val = CreateDirection(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitDirections(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                AddDirectionIfNotExist(unitOfWork.DirectionRepository, new Guid("C04304AB-BD23-4CFA-8C31-424F498034FF"),
                "Аттестация сотрудников", "АС", "АС");
                AddDirectionIfNotExist(unitOfWork.DirectionRepository, new Guid("B32F8D0D-1E62-492C-889C-FDC23F16EF88"),
                "Аттестация преподавателей", "АП", "Атт. преод.");
                AddDirectionIfNotExist(unitOfWork.DirectionRepository, new Guid("9D03AD09-0CB9-428B-B323-ED05740674B1"),
                "Единый государственный экзамен", "ЕГЭ", "ЕГЭ");
                unitOfWork.Commit();
            }
        }

        private static Duration CreateDurations(Guid id, string name, string abbreviation, string shortname)
        {
            var val = new Duration(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddDurationIfNotExist(IDurationRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            var val = rep.Get(id);
            if (val == null)
            {
                val = CreateDurations(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitDurations(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                AddDurationIfNotExist(unitOfWork.DurationRepository, new Guid("03B809F7-7E6E-49E3-9897-B90E64793B8B"),
                "48 часов", "48 ч.", "48 ч.");
                AddDurationIfNotExist(unitOfWork.DurationRepository, new Guid("C5316F56-DE41-404C-8EE0-F82EA44F9E50"),
                "96 часов", "96 ч.", "96 ч.");
                AddDurationIfNotExist(unitOfWork.DurationRepository, new Guid("842EE98B-60BE-456E-92A7-B958E7323593"),
                "от 72 до 500 часов", "72-500 ч.", "72-500 ч.");
                unitOfWork.Commit();
            }
        }


        private static FormEducation CreateFormEducations(Guid id, string name, string abbreviation, string shortname)
        {
            var val = new FormEducation(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddFormEducationIfNotExist(IFormEducationRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            var val = rep.Get(id);
            if (val == null)
            {
                val = CreateFormEducations(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitFormEducation(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                AddFormEducationIfNotExist(unitOfWork.FormEducationRepository, new Guid("FE344DC7-E157-4C6F-A667-3616A5593C3E"),
                "очная", "o", "оч.");
                AddFormEducationIfNotExist(unitOfWork.FormEducationRepository, new Guid("6F2BE360-806D-441C-BA16-FC9BCEA7D916"),
                "заочная", "з", "заоч.");
                AddFormEducationIfNotExist(unitOfWork.FormEducationRepository, new Guid("78F7EEE2-C3C6-4575-AE8D-2A774EF1B424"),
                "заочная с ДОТ", "з. ДОТ", "заоч. с ДОТ");
                unitOfWork.Commit();
            }
        }


        private static Language CreateLanguages(Guid id, string name, string abbreviation, string shortname)
        {
            var val = new Language(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddLanguageIfNotExist(ILanguageRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            var val = rep.Get(id);
            if (val == null)
            {
                val = CreateLanguages(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitLanguages(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                AddLanguageIfNotExist(unitOfWork.LanguageRepository, new Guid("758D2489-2F7C-4F15-ADFF-052685BDD877"),
                "Английский", "Анг", "Анг.");

                AddLanguageIfNotExist(unitOfWork.LanguageRepository, new Guid("831EDA7B-A0DF-4E78-8F01-4A941B8C60FB"),
                "Арабский", "Арб", "Арб.");

                AddLanguageIfNotExist(unitOfWork.LanguageRepository, new Guid("473553A8-1FA4-4237-93AE-60B5A5639101"),
                "Персидский (фарси)", "Пер", "Пер.");

                AddLanguageIfNotExist(unitOfWork.LanguageRepository, new Guid("F2EEE369-D653-4784-9062-752D8A86740F"),
                "Итальянский", "Итл", "Итл.");

                AddLanguageIfNotExist(unitOfWork.LanguageRepository, new Guid("E0A1F542-0745-47EF-96E5-9C1DE097000B"),
                "Испанский", "Исп", "Исп.");

                AddLanguageIfNotExist(unitOfWork.LanguageRepository, new Guid("5ADF85F6-7178-42E2-9359-A38980697CAD"),
                "Китайский", "Кит", "Кит.");

                AddLanguageIfNotExist(unitOfWork.LanguageRepository, new Guid("87CBDC32-CAF1-4981-8C23-B3CEF924C2E7"),
                "Чешский", "Чеш", "Чеш.");

                AddLanguageIfNotExist(unitOfWork.LanguageRepository, new Guid("F747A355-794A-4775-BAAC-BDEC96A0E979"),
                "Русский", "Рус", "Рус.");

                AddLanguageIfNotExist(unitOfWork.LanguageRepository, new Guid("3690B3BE-DA06-4558-86CC-D595736BF6BE"),
                "Немецкий", "Нем", "Нем.");

                AddLanguageIfNotExist(unitOfWork.LanguageRepository, new Guid("B47B6627-F854-469B-98BE-E11E705954A6"),
                "Французский", "Фр", "Фр.");
                unitOfWork.Commit();
            }
        }


        private static Qualification CreateQualifications(Guid id, string name, string abbreviation, string shortname)
        {
            var val = new Qualification(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddQualificationIfNotExist(IQualificationRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            var val = rep.Get(id);
            if (val == null)
            {
                val = CreateQualifications(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitQualifications(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                AddQualificationIfNotExist(unitOfWork.QualificationRepository, new Guid("216675CA-8436-443E-9D67-5CF0E01CF5D5"),
                "бакалавр", "б", "бак.");
                AddQualificationIfNotExist(unitOfWork.QualificationRepository, new Guid("CD2A3C0E-85A9-4E24-A761-8C1FFFF40FAE"),
                "специалист", "с", "спец.");
                AddQualificationIfNotExist(unitOfWork.QualificationRepository, new Guid("CD1BD0FA-FF68-47C1-9CF4-6EA268BE0C43"),
                "магистр", "м", "маг.");
                unitOfWork.Commit();
            }
        }


        private static RateEducation CreateRateEducations(Guid id, string name, string abbreviation, string shortname)
        {
            var val = new RateEducation(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddRateEducationIfNotExist(IRateEducationRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            var val = rep.Get(id);
            if (val == null)
            {
                val = CreateRateEducations(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitRateEducations(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                AddRateEducationIfNotExist(unitOfWork.RateEducationRepository, new Guid("142DA5BD-636F-4B96-B508-8CA934E022C7"),
                "среднее (полное) общее", "СО", "СО");
                AddRateEducationIfNotExist(unitOfWork.RateEducationRepository, new Guid("55940882-2774-45D2-97ED-1B6D1F50FA5B"),
                "среднее профессиональное", "СПО", "СПО");
                AddRateEducationIfNotExist(unitOfWork.RateEducationRepository, new Guid("77689B90-87A7-4686-8FD9-4AAFCA1BDBD4"),
                "высшее", "ВПО", "ВПО");
                unitOfWork.Commit();
            }
        }


        private static Specialization CreateSpecializations(Guid id, string name, string abbreviation, string shortname)
        {
            var val = new Specialization(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddSpecializationIfNotExist(ISpecializationRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            var val = rep.Get(id);
            if (val == null)
            {
                val = CreateSpecializations(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitSpecializations(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                AddSpecializationIfNotExist(unitOfWork.SpecializationRepository, new Guid("DFBD0E3F-FC84-4EB1-8C40-E540A655ADFB"),
                "Дополнительное образование", "ДО", "Доп. обр.");
                unitOfWork.Commit();
            }
        }


        private static Territory CreateTerritories(Guid id, string name, string abbreviation, string shortname)
        {
            var val = new Territory(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddTerritoryIfNotExist(ITerritoryRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            var val = rep.Get(id);
            if (val == null)
            {
                val = CreateTerritories(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitTerritories(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                AddTerritoryIfNotExist(unitOfWork.TerritoryRepository, new Guid("AB279979-08C8-4B81-B299-29A9513FD4A7"),
                "Уфа", "", "");
                unitOfWork.Commit();
            }
        }


        private static TypeTesting CreateTypeTesting(Guid id, string name, string abbreviation, string shortname)
        {
            var val = new TypeTesting(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddTypeTestingIfNotExist(ITypeTestingRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            var val = rep.Get(id);
            if (val == null)
            {
                val = CreateTypeTesting(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitTypeTesting(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                AddTypeTestingIfNotExist(unitOfWork.TypeTestingRepository, new Guid("d121aa27-6cdd-4dc6-80c0-bf9a022aa68d"),
                "Зачёт", "З", "Зач.");

                AddTypeTestingIfNotExist(unitOfWork.TypeTestingRepository, new Guid("c41027ce-d2b4-4a97-b1bf-d22a181f9636"),
                "Экзамен", "Э", "Экз.");

                AddTypeTestingIfNotExist(unitOfWork.TypeTestingRepository, new Guid("5a7d2c41-cee1-430d-9f4a-e334af08e6ec"),
                "Диффиренцированный зачёт", "Д.з.", "Дифф. зачёт");
                unitOfWork.Commit();
            }
        }


        private static Subject CreateSubject(Guid id, string name, string abbreviation, string shortname)
        {
            Subject val = new SimpleSubject() { Id = id, Name = name, ShortName = shortname, Abbreviation = abbreviation};
            return val;
        }

        private static void AddSubjectIfNotExist(ISubjectRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            var val = rep.Get(id);
            if (val == null)
            {
                val = CreateSubject(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void AddForegginSubjectIfNotExist(ISubjectRepository subRep, ILanguageRepository langRep, Guid id, string name, string abbreviation, string shortname)
        {
            var val = subRep.Get(id);
            if (val == null)
            {
                var baseSubject = new BaseForeignLanguageSubject();
                baseSubject.Name = name;
                baseSubject.Id = id;
                baseSubject.Abbreviation = abbreviation;
                baseSubject.ShortName = shortname;
                foreach (var language in langRep.GetQuery().ToList())
                {
                    var langugetSubject = new LangugetSubject();
                    langugetSubject.Name = $"{baseSubject.Name} ({language.Name})";
                    langugetSubject.Abbreviation = $"{baseSubject.Abbreviation} ({language.Abbreviation})";
                    langugetSubject.ShortName = $"{baseSubject.ShortName} ({language.ShortName})";
                    langugetSubject.Id = Guid.NewGuid();
                    langugetSubject.BaseSubject = baseSubject;
                    langugetSubject.Language = language;
                    baseSubject.LangugetSubjects.Add(langugetSubject);
                }
                subRep.Save(baseSubject);
            }
        }

        private static readonly Guid subjectPaterns = new Guid("E16E0836-3F90-4F0B-9D69-6081BB737027");
        private static readonly Guid subjectPEFCF = new Guid("2F8E6061-EBCB-43BA-8153-4643CC9E927E");
        private static readonly Guid subjectFS = new Guid("7bff1ab5-1c29-4af9-83ec-b174b3a27432");

        private static void InitSubjects(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                AddSubjectIfNotExist(unitOfWork.SubjectRepository, subjectPaterns,
                "БПЛА и угрозы от них", "БПЛА и угрозы от них", "БПЛА и угрозы от них");

                AddSubjectIfNotExist(unitOfWork.SubjectRepository, subjectPEFCF,
                "Радиоэлектронное противодействие БПЛА", "Радиоэлектронное противодействие БПЛА", "Радиоэлектронное противодействие БПЛА");

                AddForegginSubjectIfNotExist(unitOfWork.SubjectRepository, unitOfWork.LanguageRepository, subjectFS,
                "Иностранный язык", "Ин.Яз.", "Иностранный");
                
                AddForegginSubjectIfNotExist(unitOfWork.SubjectRepository, unitOfWork.LanguageRepository, new Guid("2e384f04-3643-4df4-bb53-21d8f777f2a6"),
                    "Существующие комплексные решения по РЭБ", "Существующие комплексные решения по РЭБ", "Существующие комплексные решения по РЭБ");
                
                AddSubjectIfNotExist(unitOfWork.SubjectRepository, new Guid("a458baa2-0747-4dd4-a59c-a588ad49629e"),
                    "Памятка поступившему", "Памятка поступившему", "Памятка поступившему");

                unitOfWork.Commit();
            }
        }


        private static Role CreateRole(Guid id, string name)
        {
            var val = new Role(id, name);
            return val;
        }

        private static void AddRoleIfNotExist(IRoleRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            var val = rep.Get(id);
            if (val == null)
            {
                val = CreateRole(id, name);
                rep.Save(val);
            }
        }

        private static void InitRoles(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                AddRoleIfNotExist(unitOfWork.RoleRepository, new Guid("892ab98a-a698-49b4-ba38-909585ac9701"),
                "admin", "", "");
                AddRoleIfNotExist(unitOfWork.RoleRepository, new Guid("97007f30-864f-41a1-bc0b-f15706435661"),
                "student", "", "");
                unitOfWork.Commit();
            }
        }

        #region Памятка поступившему
        private static void InitMyStoryTemplate(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var id = new Guid("6a1db7d6-40a5-4abb-9d46-211a9d6f3420");

                var template = unitOfWork.StoryTemplateRepository.Get(id);
                if (template == null)
                {

                    template = new MyStoryTemplate
                    {
                        Id = id,
                        Title = "Памятка поступившему",
                        Description =
                            "Памятка поступившему",
                        Subject = unitOfWork.SubjectRepository.Get(new Guid("a458baa2-0747-4dd4-a59c-a588ad49629e")),
                        Semester = 1,
                        TypeTesting = unitOfWork.TypeTestingRepository.Get(new Guid("d121aa27-6cdd-4dc6-80c0-bf9a022aa68d"))
                    };

                    var tmpId = new Guid("243986BC-8DD1-4587-8C63-D1380278781C");
                    MyStoryTemplateItem tempItem = MyStoryTemplateHtml.Create("theme 01",
                        "Тема 01\n<b>Тема номер 01 @beetybee</b>", template.Items.Count, tmpId);
                    template.Items.Add(tempItem);

                    tmpId = new Guid("C2D1C393-B171-4BBD-81F7-68A0A4F36CD8");
                    tempItem = MyStoryTemplateVenue.Create("ул. Тальковая, д. 36",
                        "Дом", 42.967269f, 47.503882f, template.Items.Count, tmpId);
                    template.Items.Add(tempItem);

                    /*

                    MyStoryTemplateFileBased tempItem = MyStoryTemplateVideo.CreateFromFile(
                        "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\00.mp4", "Обращение ректора О.А. Баулина к первокурсникам",
                        new Guid("89ECCD37-FB7D-43EF-82EC-1EF55F876CF6"), template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tempItem.Id,"D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\00.mp4"));
                    
                    var tmpId = new Guid("33D598AC-FFCE-405A-8F7F-8EDF1FBC7D6C");
                    var filePath = "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\01.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Памятка поступившему",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("ACD466A9-00FF-4032-9472-F113C1654A03");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\02.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Памятка поступившему",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("4039AC29-7182-4270-B32F-C2EF56FF312E");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\03.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Памятка поступившему",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("F6FE2B1A-6635-4826-A3D6-61510F52307F");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\04.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Памятка поступившему",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("847FEF0C-690F-471A-8B02-823F559DD885");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\05.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Памятка поступившему",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("CD148916-32BC-4AF5-8766-B6E050B93431");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\06.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Памятка поступившему",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));

                    tmpId = new Guid("868F96A3-6F17-4EDC-8663-C77CBF909EB3");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\07.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Памятка поступившему",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("82D9A232-F903-4E3F-846F-7A87872948EB");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\08.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Памятка поступившему",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("C317779E-F181-4638-94EA-FE48EB066518");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\09.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Памятка поступившему",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("89CDF587-D2FD-4553-85AC-7BD61AF7D889");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\10.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Памятка поступившему",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("5A669B9C-55BE-41D7-92DD-63909E441FC3");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\11.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Памятка поступившему",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("BC73DE27-14D4-4C44-BD28-C3E462996BE6");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\slides\\abi\\12.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Памятка поступившему",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    
                    template.Items.Add(MyStoryTemplateQuiz.Create("Где можно узнать расписание?",
                        new[]
                        {
                            "на сайте respisane.rusoil.net",
                            "в деканате",
                            "все ответы верны",
                        }, 2, template.Items.Count, new Guid("3D3E06E5-2E90-4335-88EA-16F008AD32DB")));
                    
                    template.Items.Add(MyStoryTemplateQuiz.Create("Где можно получить выписку из приказа о зачислении, где имеется информация, что ты являешься студентом?",
                        new[]
                        {
                            "В деканате",
                            "В приемной комиссии",
                            "В холе общежития",
                        }, 1, template.Items.Count, new Guid("6677C7F7-DE11-4B48-9EFD-31C72C9E71EC")));
                    

                    template.Items.Add(MyStoryTemplateQuiz.Create("Можно поучавствовать в «субботнике» по благоустройству учебных корпусов и общежитий, если ты",
                        new[]
                        {
                            "Первокурсник очной формы обучения",
                            "Обучаешься на заочной форме обучения",
                            "Магистрант",
                        }, 0, template.Items.Count, new Guid("7EFE7984-32D6-4715-9C06-701607F3A12A")));

                    */

                    unitOfWork.StoryTemplateRepository.Save(template);
                }

                unitOfWork.Commit();
            }
        }

        #endregion

        #region БПЛА и угрозы от них
        private static void InitMyStoryTemplateAntidron01(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var id = new Guid("9ed7be25-ed3f-4cf5-b1eb-baff9950e953");

                var template = unitOfWork.StoryTemplateRepository.Get(id);
                if (template == null)
                {

                    template = new MyStoryTemplate
                    {
                        Id = id,
                        Title = "Антидрон. БПЛА и угрозы от них",
                        Description =
                            "АСПЕКТЫ ИСПОЛЬЗОВАНИЯ СИСТЕМ ОБНАРУЖЕНИЯ И ПРОТИВОДЕЙСТВИЯ БПЛА НА ОБЪЕКТАХ КРИТИЧЕСКОЙ ИНФРАСТРУКТУРЫ",
                        Subject = unitOfWork.SubjectRepository.Get(subjectPaterns),
                        Semester = 1,
                        TypeTesting = unitOfWork.TypeTestingRepository.Get(new Guid("d121aa27-6cdd-4dc6-80c0-bf9a022aa68d"))
                    };

                    var tmpId = new Guid("8c5f92e0-caf0-46a9-a759-527cede7534b");
                    var filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\02.png";
                    MyStoryTemplateFileBased tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));


                    tempItem = MyStoryTemplateVideo.CreateFromFile(
                        "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\03.mp4", "БПЛА и угрозы от них",
                        new Guid("ffb6f296-e43e-411b-9c18-fba65043105d"), template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tempItem.Id, "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\03.mp4"));

                    tmpId = new Guid("15b436ef-008b-4877-989b-5c9cc7baa383");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\10.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tmpId = new Guid("a34f65c7-521e-48e4-8585-8b6bb6f1cb48");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\11.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));


                    tmpId = new Guid("19604D8B-2D68-4669-A821-07700525D1E3");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\12.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tmpId = new Guid("CFE18B29-B246-4E01-9772-94A7BA6BB63C");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\13.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tmpId = new Guid("4D51E617-FFD9-493C-BA1C-26DDF7F5B460");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\14.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));


                    tempItem = MyStoryTemplateVideo.CreateFromFile(
                        "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\15.mp4", "БПЛА и угрозы от них",
                        new Guid("e05c3c33-e736-4958-be31-c149381e4e67"), template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tempItem.Id, "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\15.mp4"));


                    tempItem = MyStoryTemplateVideo.CreateFromFile(
                        "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\16.mp4", "БПЛА и угрозы от них",
                        new Guid("24365760-d602-4c34-b0a7-6b9b0bd30d71"), template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tempItem.Id, "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\16.mp4"));

                    tempItem = MyStoryTemplateVideo.CreateFromFile(
                        "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\17.mp4", "БПЛА и угрозы от них",
                        new Guid("e5428792-f891-4cfa-8cb2-a740d157415c"), template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tempItem.Id, "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\17.mp4"));

                    tmpId = new Guid("3C72D3F3-8482-4125-9580-019CBC1D4C32");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\18.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tmpId = new Guid("189013F2-4657-40C6-A8B4-7E7056D33868");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\19.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));


                    tmpId = new Guid("7d520868-98c3-4d40-896c-b89c263a8298");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\19_01.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));


                    tmpId = new Guid("857d8394-0c5a-42e4-b124-7d9a05db22bb");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\19_02.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tmpId = new Guid("8ec34b77-64ff-4cc5-a74a-5580befec4c5");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\19_03.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));


                    tmpId = new Guid("A7BF8FE9-10E3-4380-AF7A-28838D2258D9");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\20.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tmpId = new Guid("0B486FC3-D0A5-4AE0-AE79-B62400703CD3");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\21.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tmpId = new Guid("BE0CBE7F-559C-4152-A445-30D052DC2DA6");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\22.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tmpId = new Guid("BFEDD2FA-8E08-4FDF-99D6-CBFC0DED0ECE");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\23.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tmpId = new Guid("391EB21C-B65D-4D56-978F-4F7AC4BFE8E7");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\24.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tmpId = new Guid("49125CA6-63D8-489F-91B4-3FE003F8E7FB");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\25.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tmpId = new Guid("170FC9BC-80FB-4D5F-A2CC-A3B5100D4E08");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\26.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tempItem = MyStoryTemplateVideo.CreateFromFile(
                        "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\27.mp4", "БПЛА и угрозы от них",
                        new Guid("4a3bc174-a5d3-43fa-ab5a-a3b5a4270110"), template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tempItem.Id, "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\27.mp4"));

                    tmpId = new Guid("702DB9CB-10C0-48D5-B00D-B59F067467B6");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\28.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tmpId = new Guid("8e615271-8af0-4185-9381-e480f615e7df");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\01\\29.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "БПЛА и угрозы от них",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    template.Items.Add(MyStoryTemplateQuiz.Create("Что тако БПЛА?",
                        new[]
                        {
                            "Большой промышленный летательный аппарат",
                            "Боевой",
                            "Беспилотный летательный аппарат",
                            "Большой планер легкой авиации"
                        }, 2, template.Items.Count, new Guid("6B0731BC-1710-4B91-962C-EFD71EEC401A")));

                    template.Items.Add(MyStoryTemplateQuiz.Create("Выберите 5 основных типов гражданских БПЛА",
                        new[]
                        {
                            "Самолетный, вертолетный, планерный, аэрографический, мультикоптерный",
                            "Самолетный, вертолетный, планерный, аэростатический, мультикоптерный",
                            "Самолетный, вертолетный, планерный, аэростатический, квадрокоптерный",
                            "Самолетный, мимолетный, планерный, аэростатический, мультикоптерный"
                        }, 1, template.Items.Count, new Guid("1961A459-A0FC-4B77-99BA-D06F20950DBA")));

                    template.Items.Add(MyStoryTemplateQuiz.Create("Выберите правильные варианты возможных поражающих элементов, которые могут нести БПЛА",
                        new[]
                        {
                            "Взрывное устройство",
                            "Гиря",
                        }, 0, template.Items.Count, new Guid("30CB9E22-C084-4C8B-8DE0-BD85C79AD411")));

                    template.Items.Add(MyStoryTemplateQuiz.Create("Выберите варианты превентивных мер минимизации угрозы.",
                        new[]
                        {
                            "Юридические и информационные, Административно-уголовные и программные",
                            "Публично-порицательные и административные, Уголовные и исправительно-трудовые",
                        }, 0, template.Items.Count, new Guid("4B9CCEAF-48FD-4456-A7B1-26D901AF3662")));

                    template.Items.Add(MyStoryTemplateQuiz.Create("Что такое РЭП?",
                        new[]
                        {
                            "Радиоэлектронный поиск",
                            "Радиоэлектронное подавление",
                            "Радиоизотопный эмиссионный прибор",
                            "Ракетно-энергетическое противодействие",
                        }, 1, template.Items.Count, new Guid("16D9944F-D504-4706-86F3-60222BF3434E")));

                    unitOfWork.StoryTemplateRepository.Save(template);
                }

                unitOfWork.Commit();
            }
        }

        #endregion

        #region Радиоэлектронное противодействие БПЛА
        private static void InitMyStoryTemplateAntidron02(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var id = new Guid("9DBF36A2-ACB2-4000-A6EA-30286EB0FFAD");

                var template = unitOfWork.StoryTemplateRepository.Get(id);
                if (template == null)
                {

                    template = new MyStoryTemplate
                    {
                        Id = id,
                        Title = "Антидрон. Радиоэлектронное противодействие БПЛА",
                        Description =
                            "АСПЕКТЫ ИСПОЛЬЗОВАНИЯ СИСТЕМ ОБНАРУЖЕНИЯ И ПРОТИВОДЕЙСТВИЯ БПЛА НА ОБЪЕКТАХ КРИТИЧЕСКОЙ ИНФРАСТРУКТУРЫ",
                        Subject = unitOfWork.SubjectRepository.Get(new Guid("2F8E6061-EBCB-43BA-8153-4643CC9E927E")),
                        Semester = 1,
                        TypeTesting = unitOfWork.TypeTestingRepository.Get(new Guid("d121aa27-6cdd-4dc6-80c0-bf9a022aa68d"))
                    };

                    
                    MyStoryTemplateFileBased tempItem = MyStoryTemplateVideo.CreateFromFile(
                        "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\24.mp4", "БПЛА и угрозы от них",
                        new Guid("7d2cdd91-c5ab-4f19-8a51-42e241922d65"), template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tempItem.Id,"D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\24.mp4"));
                    
                    var tmpId = new Guid("E7AAEE4C-19D3-4799-B7E5-4482019A7142");
                    var filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\25.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));


                    tmpId = new Guid("54333900-D64E-4578-89AE-7FD099DFC40F");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\26.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("7C3BA51D-AA86-4ED3-98F1-F4451BCB7F6A");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\27.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    
                    tmpId = new Guid("F2080B06-7360-47EF-AD7F-420AD34117AC");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\28.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("60CE8FD0-BC88-4816-BE64-345BE6234765");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\29.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("3D8962E2-BE7A-44DD-877B-481C4E141130");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\30.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                   
                    tmpId = new Guid("C82F10B9-D921-4AE2-9C09-1F84100A6439");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\31.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));

                    tmpId = new Guid("D3BCB082-D1FE-4E75-9AE0-27DCF63C00BE");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\32.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("660F9EC6-514A-4CB2-80A5-2A573F183C9F");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\33.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("5160CEEA-825E-42D6-913C-0612B0722375");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\34.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("8FA87A8F-CD8C-4C8C-BDE0-5A02717B4A3B");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\35.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("D69A240F-FF9B-4C39-9D8E-83BF09774F0F");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\36.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("BDEF9D6F-8A8A-49FD-BA95-46A9F625D8A1");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\37.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("C7835C8C-5D90-44DF-94C4-EC2CBB735A65");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\38.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("D3F1622B-C10C-4AE5-8E88-0A2F939CE898");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\39.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    
                    tmpId = new Guid("F4577F4C-0476-4D31-AA98-B9B685227C32");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\40.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("3092F1DB-A6C1-4C3E-9BE3-E4602BA133BB");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\41.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("6E721154-F773-4EA9-AF05-9DC53DF1D5FB");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\42.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("55973E1A-95B8-40BA-A78F-B3C509B4A749");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\43.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("16101F75-7FC9-4A7B-A4A2-01CCE522C95F");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\44.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("968A84C9-58D7-4D7A-B7C1-7F7B8D3A428B");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\02\\45.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Радиоэлектронное противодействие БПЛА",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    template.Items.Add(MyStoryTemplateQuiz.Create("Какие функции выполняет система РЭП?",
                        new[]
                        {
                            "мониторинг, отображение, распознавание, позиционирование, целеуказание, подавление, подтверждение",
                            "мониторинг, обнаружение, распознавание, позирование, целеуказание, подавление, подтверждение",
                            "мониторинг, отображение, распознавание, позиционирование, целеуказание, подавление, утверждение",
                            "мониторинг, отображение, распознавание, позиционирование, целеотказание, подавление, подтверждение"
                        }, 0, template.Items.Count, new Guid("5676E1DC-D211-4F61-A892-5923728A53BA")));
                    
                    template.Items.Add(MyStoryTemplateQuiz.Create("Максимальная дальность работы радиолокационного обнаружения?",
                        new[]
                        {
                            "5",
                            "11",
                            "15",
                            "21"
                        }, 3, template.Items.Count, new Guid("2F880C30-F779-41DE-8EF4-2A80B1805D23")));
                    
                    template.Items.Add(MyStoryTemplateQuiz.Create("Максимальная дальность работы купольных средств подавления?",
                        new[]
                        {
                            "5",
                            "2,5",
                            "1,2",
                            "3",
                        }, 1, template.Items.Count, new Guid("3DD257FA-ADDE-4FBF-A619-4D041CB9CE25")));
                    
                    template.Items.Add(MyStoryTemplateQuiz.Create("Выберите примерный частотный диапазон средств подавления",
                        new[]
                        {
                            "500 – 8000 МГц",
                            "400 - 6000 МГц",
                            "800 - 5600 МГц",
                            "200 - 2400 МГц",
                        }, 1, template.Items.Count, new Guid("26DC62AD-6292-4FF1-A98B-8F7D414FCBDD")));
                    
                    template.Items.Add(MyStoryTemplateQuiz.Create("Как неправильно и как правильно размещать средства РЭП",
                        new[]
                        {
                            "Внутри защищенного периметра",
                            "Внутри защищенного периметра строго по центру",
                            "Внутри защищенного периметра по краям территории",
                            "Снаружи защищенного периметра на вышке",
                        }, 2, template.Items.Count, new Guid("7C3BAED2-B6AC-4009-992F-E170F483EB2D")));
                    
                    unitOfWork.StoryTemplateRepository.Save(template);
                }

                unitOfWork.Commit();
            }
        }

        #endregion

        #region Существующие комплексные решения по РЭБ
        private static void InitMyStoryTemplateAntidron03(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var id = new Guid("BF6C93DC-9EB4-4B15-AF86-E82228A5993C");

                var template = unitOfWork.StoryTemplateRepository.Get(id);
                if (template == null)
                {

                    template = new MyStoryTemplate
                    {
                        Id = id,
                        Title = "Антидрон. Существующие комплексные решения по РЭБ",
                        Description =
                            "АСПЕКТЫ ИСПОЛЬЗОВАНИЯ СИСТЕМ ОБНАРУЖЕНИЯ И ПРОТИВОДЕЙСТВИЯ БПЛА НА ОБЪЕКТАХ КРИТИЧЕСКОЙ ИНФРАСТРУКТУРЫ",
                        Subject = unitOfWork.SubjectRepository.Get(new Guid("2e384f04-3643-4df4-bb53-21d8f777f2a6")),
                        Semester = 1,
                        TypeTesting = unitOfWork.TypeTestingRepository.Get(new Guid("d121aa27-6cdd-4dc6-80c0-bf9a022aa68d"))
                    };

                    var tmpId = new Guid("0C96F496-4844-4F5B-BBEB-3CE138672B9F");
                    var filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\01.png";
                    MyStoryTemplateFileBased tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Существующие комплексные решения по РЭБ",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));


                    tmpId = new Guid("AA0CA0E8-A285-45E5-9443-2AB2E5A87242");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\02.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Существующие комплексные решения по РЭБ",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));
                    
                    tmpId = new Guid("BCE534E6-8EF8-46AA-B87D-81B06AC4E5F9");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\03.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Существующие комплексные решения по РЭБ",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId,filePath));


                    tempItem = MyStoryTemplateVideo.CreateFromFile(
                        "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\03_01 Тест ружей 1.mp4", "Существующие комплексные решения по РЭБ",
                        new Guid("57901D6B-B052-4487-A4AB-4510907434B4"), template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tempItem.Id, "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\03_01 Тест ружей 1.mp4"));

                    tempItem = MyStoryTemplateVideo.CreateFromFile(
                        "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\03_01 Тест ружей 2.mp4", "Существующие комплексные решения по РЭБ",
                        new Guid("B8A430C6-27F4-4AF2-A4FB-E34EA710C332"), template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tempItem.Id, "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\03_01 Тест ружей 2.mp4"));

                    tempItem = MyStoryTemplateVideo.CreateFromFile(
                        "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\03_01 Тест ружей 3.mp4", "Существующие комплексные решения по РЭБ",
                        new Guid("D0C8E8F1-0F3F-4F23-9551-A25EAF77CEE4"), template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tempItem.Id, "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\03_01 Тест ружей 3.mp4"));

                    tmpId = new Guid("85C46B78-DCC7-4301-8FA8-34B77BC1DE36");
                    filePath = "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\04.png";
                    tempItem = MyStoryTemplateImage.CreateFromFile(filePath
                        , "Существующие комплексные решения по РЭБ",
                        tmpId, template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tmpId, filePath));

                    tempItem = MyStoryTemplateVideo.CreateFromFile(
                        "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\05.mp4", "Существующие комплексные решения по РЭБ",
                        new Guid("C25FA2FF-AF07-460F-83CC-E409A28B76B0"), template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tempItem.Id, "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\05.mp4"));

                    tempItem = MyStoryTemplateVideo.CreateFromFile(
                        "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\06.mp4", "Существующие комплексные решения по РЭБ",
                        new Guid("8F70AF0F-E7E1-4AB6-BB06-ECC50A90EAAD"), template.Items.Count);
                    template.Items.Add(tempItem);
                    unitOfWork.BinaryDataRepository.Save(BinaryData.CreateFromFile(
                        tempItem.Id, "D:\\yandex\\YandexDisk\\Изображения\\Слайды Антидрон\\03\\06.mp4"));

                                       
                    
                    
                    template.Items.Add(MyStoryTemplateQuiz.Create("К системам обнаружения относятся",
                        new[]
                        {
                            "Луч",
                            "ПАРЛС",
                            "Купол",
                            "Днепр"
                        }, 1, template.Items.Count, new Guid("1E96A6A7-7AE7-4CC6-91AF-B7589066E196")));
                    
                    template.Items.Add(MyStoryTemplateQuiz.Create("NeoBooster служит для",
                        new[]
                        {
                            "Усиления сигнала дронов",
                            "Создания ложных целей",
                            "Манипуляции над средствами обнаружения",
                        }, 0, template.Items.Count, new Guid("C7EB6338-3C7E-441A-86A8-ECC42080DDED")));
                    
                    
                    
                    unitOfWork.StoryTemplateRepository.Save(template);
                }

                unitOfWork.Commit();
            }
        }

        #endregion

        //private static void SaveCourseIfNotExist(Course course)
        //{
        //    var courseRepository = IoC.Resolve<ICourseRepository>();
        //    Course val = courseRepository.Get(course.Id);
        //    if (val == null)
        //    {
        //        courseRepository.Save(course);
        //    }
        //}

        //private static void InitCourses()
        //{

        //    var courseRepository = IoC.Resolve<ICourseRepository>();
        //    var subjectRepository = IoC.Resolve<ISubjectRepository>();
        //    var unitOfWorkFactory = IoC.Resolve<IUnitOfWorkFactory>();
        //    using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
        //    {
        //        Course firstCourse = new SimpleCourse();
        //        firstCourse.Id = new Guid("8D8AE35E-3038-4167-A1CF-8E70D3D7701A");
        //        firstCourse.Parent = null;
        //        firstCourse.Subject = subjectRepository.Get(subjectPaterns);
        //        Module firstModule = new SimpleModule();
        //        firstModule.Id = new Guid("A764994C-D570-455A-9737-EE3D63D8332E");
        //        firstModule.Number = 1;
        //        firstModule.Parent = firstCourse;
        //        firstModule.Description = "Стратегия";
        //        firstModule.Attestation = new Attestation() { Id = new Guid("E859B3CD-1DE8-473A-8A88-CF7B85F1BC14"), State = AttestationState.NotExecuting, TypeTesting = TypeTesting.Test };

        //        Module secondModule = new SimpleModule();
        //        secondModule.Id = new Guid("350835E7-CAD2-4D0B-9809-BFC45569712A");
        //        secondModule.Number = 2;
        //        secondModule.Parent = firstCourse;
        //        secondModule.Description = "Наблюдатель";
        //        secondModule.Attestation = new Attestation() { Id = new Guid("FBD3A1EF-E675-4876-ABD5-D375F7B35BE0"), State = AttestationState.NotExecuting, TypeTesting = TypeTesting.Exam };

        //        firstCourse.Modules.Add(firstModule);
        //        firstCourse.Modules.Add(secondModule);

        //        SaveCourseIfNotExist(firstCourse);
        //        unitOfWork.Commit();
        //    }
        //}

        private static void InitOrganization(IUnitOfWorkFactory unitOfWorkFactory)
        {
            var orgId = new Guid("C71096D3-CB28-4AA8-9AB2-32D03B4167B4");

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                //AddSpecializationIfNotExist(rep, new Guid("DFBD0E3F-FC84-4EB1-8C40-E540A655ADFB"),
                //"Дополнительное образование", "ДО", "Доп. обр.");
                var organiz = unitOfWork.OrganizationRepository.Get(orgId);
                if (organiz == null)
                {
                    var org = new Organization()
                    {
                        Id = orgId,
                        Name = "ПроЗнание",
                        Abbreviation = "ПроЗнание",
                        ShortName = "ПроЗнание"
                    };
                    org.Subdivisions.Add(new Subdivision()
                    {
                        Id = new Guid("AB279979-08C8-4B81-B299-29A9513FD4A7"),
                        Name = "Уфа",
                        Abbreviation = "",
                        ShortName = "",
                        Organization = org
                    });
                    unitOfWork.OrganizationRepository.Save(org);
                }
                unitOfWork.Commit();
            }
        }

        private static void InitTimur(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var timur_id = new Guid("8F57BF25-E38B-4BD2-97DD-A4529FCDCEFE");

                var timur = unitOfWork.PersonRepository.Get(timur_id);
                if (timur == null)
                {
                    timur = new Person(timur_id)
                    {
                        Active = true,
                        Birthday = new DateTime(1984, 9, 25).ToUniversalTime(),
                        Email = "lean@live.ru",
                        LastActivityDateTime = null,
                        LastName = "Гирфанов",
                        Login = "timur",
                        Name = "Тимур",
                        Password = "123",
                        Patronymic = "Флюрович",
                        PhotoID = Guid.Empty,
                        Sex = Sex.Man,
                        WhenSetPassWord = null
                    };

                    timur.Roles.Add(unitOfWork.RoleRepository.Get(new Guid("892ab98a-a698-49b4-ba38-909585ac9701")));
                    //timur.Roles.Add(Role.admin);
                    unitOfWork.PersonRepository.Save(timur);
                }
                unitOfWork.Commit();
            }
        }

        private static void InitPushkin(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var pushkinId = new Guid("2f21b134-f996-4205-86fd-05fa36f0ad0f");
                var pushkin = unitOfWork.PersonRepository.Get(pushkinId);
                if (pushkin == null)
                {
                    var org = unitOfWork.OrganizationRepository.Get(new Guid("C71096D3-CB28-4AA8-9AB2-32D03B4167B4"));

                    pushkin = new Person(pushkinId);

                    pushkin.Active = true;
                    pushkin.Birthday = new DateTime(1984, 9, 25).ToUniversalTime();
                    pushkin.Email = "timbox17@yandex.ru";
                    pushkin.LastActivityDateTime = null;
                    pushkin.LastName = "Пушкин";
                    pushkin.Login = "1577631715";
                    pushkin.Name = "Александр";
                    pushkin.Password = "1234";
                    pushkin.Patronymic = "Сергеевич";
                    pushkin.PhotoID = Guid.Empty;
                    pushkin.Sex = Sex.Man;
                    pushkin.WhenSetPassWord = null;
                    pushkin.Student = new Student(pushkinId, pushkin)
                    {
                        ActiveAgreement = true,
                        AgreementDate = new DateTime(2015, 9, 1).ToUniversalTime(),
                        AgreementNumber = "12313321123",
                        AgreementComment = "",
                        BaseRateEducation = unitOfWork.RateEducationRepository.Get(new Guid("142DA5BD-636F-4B96-B508-8CA934E022C7")),
                        Direction = unitOfWork.DirectionRepository.Get(new Guid("C04304AB-BD23-4CFA-8C31-424F498034FF")),
                        Duration = unitOfWork.DurationRepository.Get(new Guid("842EE98B-60BE-456E-92A7-B958E7323593")),
                        EndRateEducation = unitOfWork.RateEducationRepository.Get(new Guid("142DA5BD-636F-4B96-B508-8CA934E022C7")),
                        FormEducation = unitOfWork.FormEducationRepository.Get(new Guid("78F7EEE2-C3C6-4575-AE8D-2A774EF1B424")),
                        MaybeAlternateRule = false,
                        Qualification = unitOfWork.QualificationRepository.Get(new Guid("216675CA-8436-443E-9D67-5CF0E01CF5D5")),
                        Semester = 1,
                        Specialization = unitOfWork.SpecializationRepository.Get(new Guid("DFBD0E3F-FC84-4EB1-8C40-E540A655ADFB")),
                        StudiedLanguage = unitOfWork.LanguageRepository.Get(new Guid("758D2489-2F7C-4F15-ADFF-052685BDD877")),
                        WhenSemesterBegin = new DateTime(2015, 9, 1).ToUniversalTime(),
                        Subdivision = org.Subdivisions.First()
                    };
                    pushkin.Roles.Add(unitOfWork.RoleRepository.Get(new Guid("97007f30-864f-41a1-bc0b-f15706435661")));
                    //pushkin.Roles.Add(Role.student);
                }
                
                if (pushkin.Student.Curriculum == null)
                {
                    pushkin.Student.Curriculum = createCurriculum(pushkin.Student, new []
                    {
                        new dymmuCurriculum(){Semester = 1, SubjectName = "Памятка поступившему", TypeTesting = "Зачёт" },
                        new dymmuCurriculum(){Semester = 1, SubjectName = "БПЛА и угрозы от них", TypeTesting = "Зачёт" },
                        new dymmuCurriculum(){Semester = 1, SubjectName = "Радиоэлектронное противодействие БПЛА", TypeTesting = "Зачёт" },
                        new dymmuCurriculum(){Semester = 1, SubjectName = "Существующие комплексные решения по РЭБ", TypeTesting = "Зачёт" },
                        //new dymmuCurriculum(){Semester = 3, SubjectName = "Programming Entity Framework- Code First", TypeTesting = "Диффиренцированный зачёт" },
                    }, unitOfWork);
                    
                }
                unitOfWork.PersonRepository.Save(pushkin);
                unitOfWork.Commit();
            }
        }

        //public static void InitCurriculum()
        //{
        //    ICurriculumRepository rep = IoC.Resolve<ICurriculumRepository>();
        //    var courseRepository = IoC.Resolve<ICourseRepository>();
        //    var unitOfWorkFactory = IoC.Resolve<IUnitOfWorkFactory>();

        //    var curriculaID = new Guid("E54A7926-08DB-4F69-8932-51E4312E5D5D");

        //    using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
        //    {
        //        var currucula = rep.Get(curriculaID);
        //        if (currucula == null)
        //        {
        //            Curriculum curriculum = new Curriculum();
        //            curriculum.Id = curriculaID;
        //            curriculum.Courses.Add(courseRepository.Get(new Guid("8D8AE35E-3038-4167-A1CF-8E70D3D7701A")));
        //            rep.Save(curriculum);
        //        }
        //        unitOfWork.Commit();
        //    }
        //}

        public static void InitDictionary(IUnitOfWorkFactory unitOfWorkFactory)
        {
            InitRoles(unitOfWorkFactory);
            InitOrganization(unitOfWorkFactory);
            InitDirections(unitOfWorkFactory);
            InitDurations(unitOfWorkFactory);
            InitFormEducation(unitOfWorkFactory);
            InitLanguages(unitOfWorkFactory);
            InitQualifications(unitOfWorkFactory);
            InitRateEducations(unitOfWorkFactory);
            InitSpecializations(unitOfWorkFactory);
            InitSubjects(unitOfWorkFactory);
            InitTypeTesting(unitOfWorkFactory);
            //InitCourses();
            //InitCurriculum();

            InitMyStoryTemplate(unitOfWorkFactory);
            
            InitMyStoryTemplateAntidron01(unitOfWorkFactory);
            InitMyStoryTemplateAntidron02(unitOfWorkFactory);
            InitMyStoryTemplateAntidron03(unitOfWorkFactory);
            
            InitTimur(unitOfWorkFactory);
            InitPushkin(unitOfWorkFactory);
        }
    }
}