Эта программа была разработана как контрольное домашнее задание 3.2, вариант 13 Анохиным Антоном БПИ2311. Она позволяет считать json файл с игроками, и изменить его. Большая часть работы была посвещена именно изменению файла. Также эта программа автосохраняет файл если с последнего изменения прошло меньше 15 секунд. Временный файл хранится рядом с exe файлом.

Никакого оповещения об автосохранении пользователю не приходит(в ТЗ про это не слова), так как по-моему мнению, с точки зрению UX, если пользователь захочет сохранить файл, то он будет сохранять его целенаправленно, а не лезть не понятно куда, чтобы найти файл, который был не понятно когда сохранен. Так как мне кажется, что автосохранение temp файла нужно скорее для восстановления файла при неожиданном завершении/более глубокой работе с программой, и бесполезно для обычного пользователя. И надоедать пользователю с сообщениями об автосохранении, о которых он не просил тоже не стоит.

Решение подключает две библиотеки классов: одна отвечает за JSON файл с игроками(согласно варианту 13), другая используется для "умного" меню.

Все мои ивенты допускают null значения, благодаря этому можно избежать Warning'ов, и так как в моем сценарии я всегда проверяю что они не нулл перед тем как запустить ивент, это не создает проблем.

Также в тз(в моем индивидуальном варианте 13) сказано, что ивент(обновление game_points) должен вызываться только при изменении кол-ва очков у ачивки, но так как уровень игрока тоже участвует в формуле подсчета game_points, я вызываю ивент и там.

Также в тз ничего не сказано про сортировку по вложенным объектам, а только по полям основных объектов, но мне кажется сортировать по кол-ву вложенных объектов(в моем случае очивок) более чем естественно.

Почему можно вводить отрицательные числа: как мне кажется, делать минимальный уровень/очков нулем неправильно, так как можно например делать антиачивки, которые будут уменьшать game_points, также можно делать уровень отрицательный, например за какое-то неподобающее поведение.

Какие дополнительные функции реализованы в этом проекте:
комплексное меню, из каждой страницы которого можно достич любую другую страницу, кнопка return на каждой странице(везде кроме ввода данных, что как мне кажется логично)
Красивый вывод содержимого файла(а не в стиле JSON)
Добавление нового игрока/ачивки, удаление
Тонкая (УДОБНАЯ!) настройка каждого поля
Возможность сгенерировать случайное имя/описание/полностью объект
Сохранение файла (Вроде очевидно, но в тз нет)