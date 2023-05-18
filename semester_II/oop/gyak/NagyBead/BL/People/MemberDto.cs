namespace BL.People
{
    public class MemberDto
    {
        public IMember Member { get; }
        public Guid Id { get; }

        public MemberDto(IMember member, Guid id)
        {
            Member = member;
            Id = id;
        }
    }
}