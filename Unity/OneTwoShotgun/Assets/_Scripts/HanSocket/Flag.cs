public class Flag
{
   bool     m_flag;
   bool     m_autoReset;
   object   m_crit;

   public void Set()
   {
      lock (m_crit)
      {
         m_flag = true;
      }
   }
   public bool Get()
   {
      lock (m_crit)
      {
         if (m_flag)
         {
            if (m_autoReset)
            {
               m_flag = false;
            }

            return true;
         }
      }

      return false;
   }

   public Flag(bool initialStatus = false, bool autoResetFlag = true)
   {
      m_flag        = initialStatus;
      m_autoReset   = autoResetFlag;
      m_crit        = new object();
   }
}