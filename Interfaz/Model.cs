using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

public class Model
{
    public ObservableCollection<Bean> listFunciones = new ObservableCollection<Bean>();

	public Model()
	{
        
	}

    public void addFunction(Bean bean)
    {
        listFunciones.Add(bean);
    }

    public void deleteFunction(Bean bean)
    {
        int pos = listFunciones.IndexOf(bean);
        listFunciones.RemoveAt(pos);
    }

    public void modifyFunction(Bean beanV, Bean beanN)
    {
        int pos = listFunciones.IndexOf(beanV);
        listFunciones.RemoveAt(pos);
        listFunciones.Insert(pos, beanN);
    }
}
