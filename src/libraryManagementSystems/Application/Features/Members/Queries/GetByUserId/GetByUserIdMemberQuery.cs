using Application.Features.Members.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Members.Queries.GetById;

public class GetByUserIdMemberQuery : IRequest<GetByUserIdMemberResponse>
{
    public Guid Id { get; set; }

    public class GetByUserIdMemberQueryHandler : IRequestHandler<GetByUserIdMemberQuery, GetByUserIdMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly MemberBusinessRules _memberBusinessRules;

        public GetByUserIdMemberQueryHandler(IMapper mapper, IMemberRepository memberRepository, MemberBusinessRules memberBusinessRules)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _memberBusinessRules = memberBusinessRules;
        }

        public async Task<GetByUserIdMemberResponse> Handle(GetByUserIdMemberQuery request, CancellationToken cancellationToken)
        {
            Member? member = await _memberRepository.GetAsync(predicate: m => m.UserId == request.Id, cancellationToken: cancellationToken);
            await _memberBusinessRules.MemberShouldExistWhenSelected(member);

            GetByUserIdMemberResponse response = _mapper.Map<GetByUserIdMemberResponse>(member);
            return response;
        }
    }
}