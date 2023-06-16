
Console.Write("Введите путь к папке в формате С:\\...\\...\\... : ");
string folderPath = Console.ReadLine();

long sizeBeforeDelete = DirSize(new DirectoryInfo(@folderPath));

if (sizeBeforeDelete == 0)
{
    Console.WriteLine("Папки не существует или она пустая");
}
else
{
    Console.WriteLine("Размер папки {0} байт.", sizeBeforeDelete);

    DeleteFiles(folderPath);
    long sizeAfterDelete = DirSize(new DirectoryInfo(@folderPath));
    Console.WriteLine("Было освобождено: {0} байт", sizeBeforeDelete - sizeAfterDelete);

    Console.WriteLine("Текущий размер: {0} байт", sizeAfterDelete);
}

static void DeleteFiles(string folderPath)
{

    DirectoryInfo folder = new DirectoryInfo(@folderPath);

    try
    {
        if (folder.Exists)
        {
            foreach (FileInfo file in folder.GetFiles())
            {
                if (file.LastAccessTime < DateTime.Now.AddMinutes(-30))
                    file.Delete();
            }


            foreach (DirectoryInfo dir in folder.GetDirectories())
            {
                if (dir.LastAccessTime < DateTime.Now.AddMinutes(-30))
                    dir.Delete(true);
            }
        }

        else
            Console.WriteLine("Такой папки не существует");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Произошла ошибка: " + ex.Message);
    }
}
static long DirSize(DirectoryInfo targetFolder)
{
    long size = 0;

    try
    {
        FileInfo[] files = targetFolder.GetFiles();
        foreach (FileInfo file in files)
            size += file.Length;


        DirectoryInfo[] subdirectories = targetFolder.GetDirectories();
        foreach (DirectoryInfo sub in subdirectories)
            size += DirSize(sub);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Произошла ошибка: " + ex.Message);
    }

    return size;
}