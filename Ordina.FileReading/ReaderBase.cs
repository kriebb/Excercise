using System;
using Decor;

namespace Ordina.FileReading
{
    internal abstract class ReaderBase<T> : IReader<T>
    {
        private readonly IPathValidations _pathValidations;

        protected ReaderBase(IPathValidations pathValidations)
        {
            _pathValidations = pathValidations;
        }

        [Decorate(typeof(RbacDecorator))]
        public T ReadContent(string path)
        {
            _pathValidations.ThrowWhenInvalid(path);

            return DoReadContent(path);
        }

        protected abstract T DoReadContent(string path);

        [Decorate(typeof(RbacDecorator))]

        public T ReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm)
        {
            _pathValidations.ThrowWhenInvalid(path);
            if (decryptionAlgorithm == null) throw new ArgumentNullException(nameof(decryptionAlgorithm));
            return DoReadContent(path, decryptionAlgorithm);
        }

        protected abstract T DoReadContent(string path, IDecryptionAlgorithm decryptionAlgorithm);

    }
}