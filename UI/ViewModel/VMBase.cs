using System;

namespace FileRacks.UI.ViewModel
{
    public class VMBase : ObservableObject, IDisposable
    {
        public VMBase()
        {

        }        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {            
            
        }        
    }
}