using AES.BusinessLogic;
using AES.BusinessLogic.Implementation;
using AES.Domain;
using AES.Infrastructure;
using Telegram.BotAPI.AvailableTypes;

namespace MyStoryBot;

public class UserUtils
{
    static IDictionary<string, TypeTesting> CreateTypeTestingDictionaryByName(ITypeTestingRepository repository)
    {
        IDictionary<string, TypeTesting> dic = new Dictionary<string, TypeTesting>();
        foreach (var element in repository.GetQuery().ToList())
        {
            dic.Add(element.Name, element);
        }

        return dic;
    }

    static IDictionary<string, Subject> CreateSubjectDictionaryByName(ISubjectRepository repository)
    {
        IDictionary<string, Subject> dic = new Dictionary<string, Subject>();
        foreach (var element in repository.GetQuery().ToList())
        {
            dic.Add(element.Name, element);
        }

        return dic;
    }

    static Curriculum createCurriculum(Student student, dymmuCurriculum[] curriculaTemplate, IUnitOfWork unitOfWork)
    {
        var subjects = CreateSubjectDictionaryByName(unitOfWork.SubjectRepository);
        var typeTesting = CreateTypeTestingDictionaryByName(unitOfWork.TypeTestingRepository);

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

    public static Person InitNewUser(IUnitOfWork unitOfWork, long telegramUserId, User user)
    {
        IUserFinder userFinder = new UserFinder(unitOfWork);
        var newUser = userFinder.findByLogin(telegramUserId.ToString());
        if (newUser == null)
        {
            var org = unitOfWork.OrganizationRepository.Get(new Guid("C71096D3-CB28-4AA8-9AB2-32D03B4167B4"));

            var firstName = user?.FirstName ?? user?.Username ?? "Новый";
            var lastName = user?.LastName ?? "Новый";
            var userId = Guid.NewGuid();
            newUser = new Person(userId);

            newUser.Active = true;
            newUser.Birthday = new DateTime(1984, 9, 25).ToUniversalTime();
            newUser.Email = telegramUserId+"@yandex.ru";
            newUser.LastActivityDateTime = null;
            newUser.LastName = lastName;
            newUser.Login = telegramUserId.ToString();
            newUser.Name = firstName;
            newUser.Password = "1234";
            newUser.Patronymic = "-";
            newUser.PhotoID = Guid.Empty;
            newUser.Sex = Sex.Man;
            newUser.WhenSetPassWord = null;
            newUser.Student = new Student(userId, newUser)
            {
                ActiveAgreement = true,
                AgreementDate = new DateTime(2015, 9, 1).ToUniversalTime(),
                AgreementNumber = telegramUserId.ToString(),
                AgreementComment = "",
                BaseRateEducation =
                    unitOfWork.RateEducationRepository.Get(new Guid("142DA5BD-636F-4B96-B508-8CA934E022C7")),
                Direction = unitOfWork.DirectionRepository.Get(new Guid("C04304AB-BD23-4CFA-8C31-424F498034FF")),
                Duration = unitOfWork.DurationRepository.Get(new Guid("842EE98B-60BE-456E-92A7-B958E7323593")),
                EndRateEducation =
                    unitOfWork.RateEducationRepository.Get(new Guid("142DA5BD-636F-4B96-B508-8CA934E022C7")),
                FormEducation =
                    unitOfWork.FormEducationRepository.Get(new Guid("78F7EEE2-C3C6-4575-AE8D-2A774EF1B424")),
                MaybeAlternateRule = false,
                Qualification =
                    unitOfWork.QualificationRepository.Get(new Guid("216675CA-8436-443E-9D67-5CF0E01CF5D5")),
                Semester = 1,
                Specialization =
                    unitOfWork.SpecializationRepository.Get(new Guid("DFBD0E3F-FC84-4EB1-8C40-E540A655ADFB")),
                StudiedLanguage = unitOfWork.LanguageRepository.Get(new Guid("758D2489-2F7C-4F15-ADFF-052685BDD877")),
                WhenSemesterBegin = new DateTime(2015, 9, 1).ToUniversalTime(),
                Subdivision = org.Subdivisions.First()
            };
            newUser.Roles.Add(unitOfWork.RoleRepository.Get(new Guid("97007f30-864f-41a1-bc0b-f15706435661")));
            //pushkin.Roles.Add(Role.student);
        }

        if (newUser.Student.Curriculum == null)
        {
            newUser.Student.Curriculum = createCurriculum(newUser.Student, new[]
            {
               // new dymmuCurriculum() { Semester = 1, SubjectName = "Памятка поступившему", TypeTesting = "Зачёт" },
                new dymmuCurriculum("БПЛА и угрозы от них", "Зачёт", 1),
                new dymmuCurriculum("Радиоэлектронное противодействие БПЛА", "Зачёт", 1),
                new dymmuCurriculum("Существующие комплексные решения по РЭБ", "Зачёт", 1),
            }, unitOfWork);
        }

        unitOfWork.PersonRepository.Save(newUser);
        return newUser;
    }

    private class dymmuCurriculum
    {
        public dymmuCurriculum(string subjectName, string typeTesting, int semester)
        {
            SubjectName = subjectName;
            TypeTesting = typeTesting;
            Semester = semester;
        }

        public string SubjectName { get; set; }
        public string TypeTesting { get; set; }
        public int Semester { get; set; }
    }
}