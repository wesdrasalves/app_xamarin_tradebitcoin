using PCLStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MercadoBitcoin.Helpers
{
    public static class PCLHelper
    {
        public async static Task<bool> ArquivoExisteAsync(this string nomeArquivo, IFolder pastaRaiz = null)
        {
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult pastaExiste = await pasta.CheckExistsAsync(nomeArquivo);
            // não sobrescreve se existir
            if (pastaExiste == ExistenceCheckResult.FileExists)
            {
                return true;
            }
            return false;
        }
        public async static Task<bool> PastaExisteAsync(this string nomeDaPasta, IFolder pastaRaiz = null)
        {
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult pastaExiste = await pasta.CheckExistsAsync(nomeDaPasta);
            // não sobrescreve se existir
            if (pastaExiste == ExistenceCheckResult.FolderExists)
            {
                return true;
            }
            return false;
        }
        public async static Task<IFolder> CriarPasta(this string nomeDaPasta, IFolder pastaRaiz = null)
        {
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;
            pasta = await pasta.CreateFolderAsync(nomeDaPasta, CreationCollisionOption.ReplaceExisting);
            return pasta;
        }
        public async static Task<IFile> CriarArquivo(this string nomeArquivo, IFolder pastaRaiz = null)
        {
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;
            IFile arquivo = await pasta.CreateFileAsync(nomeArquivo, CreationCollisionOption.ReplaceExisting);
            return arquivo;
        }
        public async static Task<bool> WriteTextAllAsync(this string nomeArquivo, string content = "", IFolder pastaRaiz = null)
        {
            IFile arquivo = await nomeArquivo.CriarArquivo(pastaRaiz);
            await arquivo.WriteAllTextAsync(content);
            return true;
        }
        public async static Task<string> ReadAllTextAsync(this string nomeArquivo, IFolder pastaRaiz = null)
        {
            string conteudo = "";
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;
            bool existe = await nomeArquivo.ArquivoExisteAsync(pasta);
            if (existe == true)
            {
                try
                {
                    IFile arquivo = await pasta.GetFileAsync(nomeArquivo);
                    conteudo = await arquivo.ReadAllTextAsync();
                }
                catch
                {
                    throw new Exception("Erro ao ler arquivo.");
                }
            }
            return conteudo;
        }
        public async static Task<bool> DeleteFile(this string nomeArquivo, IFolder pastaRaiz = null)
        {
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;
            bool existe = await nomeArquivo.ArquivoExisteAsync(pasta);
            if (existe == true)
            {
                IFile arquivo = await pasta.GetFileAsync(nomeArquivo);
                await arquivo.DeleteAsync();
                return true;
            }
            return false;
        }
        public async static Task SaveImagem(this byte[] imagem, String nomeArquivo, IFolder pastaRaiz = null)
        {
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;
            // cria o arquivo e sobrescreve 
            IFile arquivo = await pasta.CreateFileAsync(nomeArquivo, CreationCollisionOption.ReplaceExisting);
            // popula o arquivo com dados da imagem
            using (System.IO.Stream stream = await arquivo.OpenAsync(FileAccess.ReadAndWrite))
            {
                stream.Write(imagem, 0, imagem.Length);
            }
        }
        public async static Task<byte[]> LoadImagem(this byte[] imagem, String nomeArquivo, IFolder pastaRaiz = null)
        {
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;
            //abre se o arquivo existir
            IFile arquivo = await pasta.GetFileAsync(nomeArquivo);
            //carrega o stream para o buffer
            using (System.IO.Stream stream = await arquivo.OpenAsync(FileAccess.ReadAndWrite))
            {
                long length = stream.Length;
                byte[] streamBuffer = new byte[length];
                stream.Read(streamBuffer, 0, (int)length);
                return streamBuffer;
            }
        }
    }
}
