using System;
using TransactionManagement.Shared;

namespace TransactionManagement.Domain
{
    public interface IParserFactory
    {
        IParser GetParser(string fileType);
    }

    public class ParserFactory : IParserFactory
    {
        public IParser GetParser(string fileType)
        {
            switch(fileType)
            {
                case ("application/vnd.ms-excel"):
                    {
                        return new CsvParser();
                    }
                case ("text/xml"):
                    {
                        return new Parsing.XmlParser();
                    }
                default:
                    throw new TransactionManagementException( "Unknown format");
            }
        }
    }
}
