namespace LibraryCore.People;

public class MemberDTO
{
    public IMember Member { get; }
    public Guid Id { get; }

    public MemberDTO(IMember member, Guid id)
    {
        Member = member;
        Id = id;
    }
}