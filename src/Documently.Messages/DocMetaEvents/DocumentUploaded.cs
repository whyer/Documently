using System;

namespace Documently.Messages.DocMetaEvents
{
	/// <summary>
	/// When the file finishes uploading from disk or network, this event is raised.
	/// </summary>
	public interface DocumentUploaded : DomainEvent
	{
		/// <summary>
		/// Gets the URI of the data. It could be something like 
		/// <code>file:///inetpub/www/App_Data/uploaded/23455553.bin</code> or
		/// <code>https://github.com/haf/Documently/raw/README.md</code>.
		/// </summary>
		Uri Data { get; }
	}
}