using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocNetMockup.Models
{
    public class Image
    {
        /// <summary>
        /// Identifier of an image.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User ID of the image owner.
        /// </summary>
        public Guid OwnerId { get; set; }

        public string Extension { get; set; }

        public string Path => $"TODO/{OwnerId}/{Id}.{Extension}";

        /// <summary>
        /// If true, this image can only be seen by members of <see cref="VisibleIn"/>.
        /// If false, <see cref="VisibleIn"/> is not taken into account and this image can be seen by anyone on the social network.
        /// Private images cannot be viewed unless the user is logged in and is a member of one (or more) of <see cref="VisibleIn"/>
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// List of peers or group chats whose members can see this picture. Others will not be able to see it even if they
        /// use the image's direct link.
        /// If <see cref="IsPrivate"/> == false, this list is not taken into account.
        /// </summary>
        [NotMapped /* FIXME */] public List<Guid> VisibleIn { get; set; }
    }
}
