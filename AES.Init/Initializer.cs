namespace AES.Init
{
    using AES.Domain;
    using AES.Infrastructure;
    using System;
    using System.Collections;
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

            Curriculum curriculum = new Curriculum()
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
                    IsRequared = true,
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
            Direction val = new Direction(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddDirectionIfNotExist(IDirectionRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            Direction val = rep.Get(id);
            if (val == null)
            {
                val = CreateDirection(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitDirections(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
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
            Duration val = new Duration(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddDurationIfNotExist(IDurationRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            Duration val = rep.Get(id);
            if (val == null)
            {
                val = CreateDurations(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitDurations(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
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
            FormEducation val = new FormEducation(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddFormEducationIfNotExist(IFormEducationRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            FormEducation val = rep.Get(id);
            if (val == null)
            {
                val = CreateFormEducations(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitFormEducation(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
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
            Language val = new Language(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddLanguageIfNotExist(ILanguageRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            Language val = rep.Get(id);
            if (val == null)
            {
                val = CreateLanguages(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitLanguages(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
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
            Qualification val = new Qualification(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddQualificationIfNotExist(IQualificationRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            Qualification val = rep.Get(id);
            if (val == null)
            {
                val = CreateQualifications(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitQualifications(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
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
            RateEducation val = new RateEducation(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddRateEducationIfNotExist(IRateEducationRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            RateEducation val = rep.Get(id);
            if (val == null)
            {
                val = CreateRateEducations(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitRateEducations(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
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
            Specialization val = new Specialization(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddSpecializationIfNotExist(ISpecializationRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            Specialization val = rep.Get(id);
            if (val == null)
            {
                val = CreateSpecializations(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitSpecializations(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                AddSpecializationIfNotExist(unitOfWork.SpecializationRepository, new Guid("DFBD0E3F-FC84-4EB1-8C40-E540A655ADFB"),
                "Дополнительное образование", "ДО", "Доп. обр.");
                unitOfWork.Commit();
            }
        }


        private static Territory CreateTerritories(Guid id, string name, string abbreviation, string shortname)
        {
            Territory val = new Territory(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddTerritoryIfNotExist(ITerritoryRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            Territory val = rep.Get(id);
            if (val == null)
            {
                val = CreateTerritories(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitTerritories(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                AddTerritoryIfNotExist(unitOfWork.TerritoryRepository, new Guid("AB279979-08C8-4B81-B299-29A9513FD4A7"),
                "Уфа", "", "");
                unitOfWork.Commit();
            }
        }


        private static TypeTesting CreateTypeTesting(Guid id, string name, string abbreviation, string shortname)
        {
            TypeTesting val = new TypeTesting(id, name, shortname, abbreviation);
            return val;
        }

        private static void AddTypeTestingIfNotExist(ITypeTestingRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            TypeTesting val = rep.Get(id);
            if (val == null)
            {
                val = CreateTypeTesting(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void InitTypeTesting(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
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
            Subject val = rep.Get(id);
            if (val == null)
            {
                val = CreateSubject(id, name, abbreviation, shortname);
                rep.Save(val);
            }
        }

        private static void AddForegginSubjectIfNotExist(ISubjectRepository subRep, ILanguageRepository langRep, Guid id, string name, string abbreviation, string shortname)
        {
            Subject val = subRep.Get(id);
            if (val == null)
            {
                BaseForeignLanguageSubject baseSubject = new BaseForeignLanguageSubject();
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
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                AddSubjectIfNotExist(unitOfWork.SubjectRepository, subjectPaterns,
                "Паттерны проектирования", "ПП", "Паттерны. проект.");

                AddSubjectIfNotExist(unitOfWork.SubjectRepository, subjectPEFCF,
                "Programming Entity Framework- Code First", "PEF-CF", "Programming EF- Code First");

                AddForegginSubjectIfNotExist(unitOfWork.SubjectRepository, unitOfWork.LanguageRepository, subjectFS,
                "Иностранный язык", "Ин.Яз.", "Иностранный");

                unitOfWork.Commit();
            }
        }


        private static Role CreateRole(Guid id, string name)
        {
            Role val = new Role(id, name);
            return val;
        }

        private static void AddRoleIfNotExist(IRoleRepository rep, Guid id, string name, string abbreviation, string shortname)
        {
            Role val = rep.Get(id);
            if (val == null)
            {
                val = CreateRole(id, name);
                rep.Save(val);
            }
        }

        private static void InitRoles(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                AddRoleIfNotExist(unitOfWork.RoleRepository, new Guid("892ab98a-a698-49b4-ba38-909585ac9701"),
                "admin", "", "");
                AddRoleIfNotExist(unitOfWork.RoleRepository, new Guid("97007f30-864f-41a1-bc0b-f15706435661"),
                "student", "", "");
                unitOfWork.Commit();
            }
        }

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
            Guid org_id = new Guid("C71096D3-CB28-4AA8-9AB2-32D03B4167B4");

            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                //AddSpecializationIfNotExist(rep, new Guid("DFBD0E3F-FC84-4EB1-8C40-E540A655ADFB"),
                //"Дополнительное образование", "ДО", "Доп. обр.");
                Organization organiz = unitOfWork.OrganizationRepository.Get(org_id);
                if (organiz == null)
                {
                    Organization org = new Organization()
                    {
                        Id = org_id,
                        Name = "Центр аттестации",
                        Abbreviation = "ЦА",
                        ShortName = ""
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
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                Guid timur_id = new Guid("8F57BF25-E38B-4BD2-97DD-A4529FCDCEFE");

                Person timur = unitOfWork.PersonRepository.Get(timur_id);
                if (timur == null)
                {
                    timur = new Person(timur_id);

                    timur.Active = true;
                    timur.Birthday = new DateTime(1984, 9, 25).ToUniversalTime();
                    timur.Email = "lean@live.ru";
                    timur.LastActivityDateTime = null;
                    timur.LastName = "Гирфанов";
                    timur.Login = "timur";
                    timur.Name = "Тимур";
                    timur.Password = "123";
                    timur.Patronymic = "Флюрович";
                    timur.PhotoID = Guid.Empty;
                    timur.Sex = Sex.Man;
                    timur.WhenSetPassWord = null;
                    timur.Roles.Add(unitOfWork.RoleRepository.Get(new Guid("892ab98a-a698-49b4-ba38-909585ac9701")));
                    //timur.Roles.Add(Role.admin);
                    unitOfWork.PersonRepository.Save(timur);
                }
                unitOfWork.Commit();
            }
        }

        private static void InitPushkin(IUnitOfWorkFactory unitOfWorkFactory)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.Create())
            {
                Guid pushkinId = new Guid("2f21b134-f996-4205-86fd-05fa36f0ad0f");
                Person pushkin = unitOfWork.PersonRepository.Get(pushkinId);
                if (pushkin == null)
                {
                    var org = unitOfWork.OrganizationRepository.Get(new Guid("C71096D3-CB28-4AA8-9AB2-32D03B4167B4"));

                    pushkin = new Person(pushkinId);

                    pushkin.Active = true;
                    pushkin.Birthday = new DateTime(1984, 9, 25).ToUniversalTime();
                    pushkin.Email = "timbox17@yandex.ru";
                    pushkin.LastActivityDateTime = null;
                    pushkin.LastName = "Пушкин";
                    pushkin.Login = "pushkin";
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
                        new dymmuCurriculum(){Semester = 1, SubjectName = "Паттерны проектирования", TypeTesting = "Зачёт" },
                        new dymmuCurriculum(){Semester = 1, SubjectName = "Иностранный язык", TypeTesting = "Зачёт" },
                        new dymmuCurriculum(){Semester = 2, SubjectName = "Programming Entity Framework- Code First", TypeTesting = "Зачёт" },
                        new dymmuCurriculum(){Semester = 2, SubjectName = "Иностранный язык", TypeTesting = "Экзамен" },
                        new dymmuCurriculum(){Semester = 3, SubjectName = "Programming Entity Framework- Code First", TypeTesting = "Диффиренцированный зачёт" },
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

            InitTimur(unitOfWorkFactory);
            InitPushkin(unitOfWorkFactory);
        }
    }
}