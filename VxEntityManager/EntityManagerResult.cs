namespace VxMySQLManager.VxEntityManager
{
    public class EntityManagerResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string ExecutedSql { get; set; }
    }
}